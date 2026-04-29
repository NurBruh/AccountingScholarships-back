using AccountingScholarships.Application.Queries.EpvoSso.EpvoJoin;
using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Infrastructure.Services.StudentSync;
using AccountingScholarships.Infrastructure.Data;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.API.Controllers.Real
{
    [ApiController]
    [Route("api/epvo")]
    public class EpvoSsoJoinController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStoredProcedureRepository _spRepo;

        public EpvoSsoJoinController(IMediator mediator, IStoredProcedureRepository spRepo)
        {
            _mediator = mediator;
            _spRepo = spRepo;
        }
        // --- Students SSO Details -------------------------------------

        /// <summary>
        /// Возвращает детальную информацию по студентам из STUDENT_SSO
        /// с расшифровкой формы обучения, языка, профессии, специализации, факультета.
        /// </summary>
        [HttpGet("students")]
        public async Task<IActionResult> GetStudentSsoDetails(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetStudentSsoDetailsQuery(), ct);
            if (result is null)
                return NotFound();
            return Ok(result);
        }
    
        public class SyncBatchRequest
        {
            public List<string> IinS { get; set; } = new();
        }

        [HttpPost("sync-batch")]
        public async Task<IActionResult> SyncBatch([FromBody] SyncBatchRequest request, [FromServices] AccountingScholarships.Application.Interfaces.IComparisonRepository comparisonRepo, CancellationToken ct)
        {
            var currentUser = User.Identity?.Name ?? "System";
            var result = await comparisonRepo.SyncBatchAsync(request.IinS, currentUser, ct);
            return Ok(result);
        }

        [HttpPost("save-temp-batch")]
        public async Task<IActionResult> SaveTempBatch([FromBody] SaveTempBatchRequest request, [FromServices] ISsoToEpvoMapperService mapperService, [FromServices] EpvoSsoDbContext epvoContext, CancellationToken ct)
        {
            if (request.Students == null || !request.Students.Any())
                return BadRequest("No students provided.");

            var iins = request.Students.Select(s => s.IIN).Where(i => !string.IsNullOrEmpty(i)).Cast<string>().ToList();
            var mappedTemp = await mapperService.MapStudentsAsync(iins, ct);

            string sessionId = Guid.NewGuid().ToString();

            foreach (var temp in mappedTemp)
            {
                var edited = request.Students.FirstOrDefault(s => s.IIN == temp.IinPlt);
                if (edited != null)
                {
                    // Apply frontend edits
                    if (!string.IsNullOrEmpty(edited.Sso_FullName))
                    {
                        var parts = edited.Sso_FullName.Split(' ');
                        temp.LastName = parts.Length > 0 ? parts[0] : temp.LastName;
                        temp.FirstName = parts.Length > 1 ? parts[1] : temp.FirstName;
                        temp.Patronymic = parts.Length > 2 ? parts[2] : temp.Patronymic;
                    }
                    if (edited.Sso_CourseNumber.HasValue) temp.CourseNumber = edited.Sso_CourseNumber.Value;
                    
                    if (edited.Sso_PaymentType == "Стипендия") temp.PaymentFormId = 2;
                    else if (edited.Sso_PaymentType == "Платник") temp.PaymentFormId = 1;

                    temp.Iic = edited.Sso_Iic;
                    temp.Bic = edited.Sso_Bic;
                }
                
                temp.SyncSessionId = sessionId;
            }

            // Delete old records for these IINs from STUDENT_TEMP
            var existing = await epvoContext.Student_Temp.Where(t => iins.Contains(t.IinPlt)).ToListAsync(ct);
            epvoContext.Student_Temp.RemoveRange(existing);
            await epvoContext.SaveChangesAsync(ct);

            // Insert new records
            await epvoContext.Student_Temp.AddRangeAsync(mappedTemp, ct);
            await epvoContext.SaveChangesAsync(ct);

            return Ok(new { Message = "Saved to STUDENT_TEMP successfully.", SessionId = sessionId, Count = mappedTemp.Count });
        }

        [HttpPost("send-temp-to-epvo-session")]
        public async Task<IActionResult> SendTempToEpvoSession([FromBody] SyncSessionRequest request, [FromServices] EpvoSsoDbContext epvoContext, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(request.SyncSessionId))
                return BadRequest("SessionId is required.");

            var tempStudents = await epvoContext.Student_Temp
                .Where(t => t.SyncSessionId == request.SyncSessionId)
                .AsNoTracking()
                .ToListAsync(ct);

            if (!tempStudents.Any())
                return NotFound($"No records found in STUDENT_TEMP for SessionId {request.SyncSessionId}");

            var triggeredBy = User.Identity?.Name ?? "System";
            var tempIds = new HashSet<int>(tempStudents.Select(s => s.StudentId));
            
            var existingDumps = await epvoContext.Student_Dumps
                .Where(d => tempIds.Contains(d.StudentId))
                .ToDictionaryAsync(d => d.StudentId, ct);
                
            var existingInfos = await epvoContext.Student_Info
                .Where(i => tempIds.Contains(i.StudentId))
                .ToDictionaryAsync(i => i.StudentId, ct);

            int success = 0;
            int errors = 0;
            var logs = new List<StudentSyncLog>();
            var changeLogs = new List<StudentChangeLog>();

            foreach (var temp in tempStudents)
            {
                var log = new StudentSyncLog
                {
                    StudentId = temp.StudentId,
                    IinPlt = temp.IinPlt,
                    SentAt = DateTime.UtcNow,
                    EpvoEndpoint = "STUDENT_DUMP/STUDENT_INFO",
                    TriggeredBy = triggeredBy
                };

                try
                {
                    // 1. Update STUDENT_DUMP
                    if (existingDumps.TryGetValue(temp.StudentId, out var dump))
                    {
                        dump.FirstName = temp.FirstName;
                        dump.LastName = temp.LastName;
                        dump.Patronymic = temp.Patronymic;
                        dump.CourseNumber = temp.CourseNumber;
                        dump.PaymentFormId = temp.PaymentFormId;
                    }
                    else
                    {
                        dump = new Student_Dump
                        {
                            StudentId = temp.StudentId,
                            IinPlt = temp.IinPlt,
                            FirstName = temp.FirstName,
                            LastName = temp.LastName,
                            Patronymic = temp.Patronymic,
                            CourseNumber = temp.CourseNumber,
                            PaymentFormId = temp.PaymentFormId
                        };
                        epvoContext.Student_Dumps.Add(dump);
                    }

                    // 2. Update STUDENT_INFO (Bank details)
                    if (temp.Iic != null || temp.Bic != null)
                    {
                        if (existingInfos.TryGetValue(temp.StudentId, out var info))
                        {
                            if (info.Iic != temp.Iic || info.Bic != temp.Bic)
                            {
                                changeLogs.Add(new StudentChangeLog { IinPlt = temp.IinPlt, FieldName = "IIC/BIC", OldValue = $"{info.Iic}/{info.Bic}", NewValue = $"{temp.Iic}/{temp.Bic}", DataSource = "SSO", ChangedBy = triggeredBy, ChangedAt = DateTime.UtcNow, SyncSessionId = request.SyncSessionId });
                                info.Iic = temp.Iic ?? info.Iic;
                                info.Bic = temp.Bic ?? info.Bic;
                                info.UpdateDate = DateOnly.FromDateTime(DateTime.Now);
                            }
                        }
                        else
                        {
                            epvoContext.Student_Info.Add(new Student_Info
                            {
                                UniversityId = temp.UniversityId.GetValueOrDefault(29),
                                StudentId = temp.StudentId,
                                Iic = temp.Iic,
                                Bic = temp.Bic,
                                UpdateDate = DateOnly.FromDateTime(DateTime.Now)
                            });
                        }
                    }

                    log.Status = "Success";
                    success++;
                }
                catch (Exception ex)
                {
                    log.Status = "Error";
                    log.ErrorMessage = ex.Message;
                    errors++;
                }
                logs.Add(log);
            }

            await epvoContext.StudentSyncLogs.AddRangeAsync(logs, ct);
            await epvoContext.StudentChangeLogs.AddRangeAsync(changeLogs, ct);
            await epvoContext.SaveChangesAsync(ct);

            return Ok(new { Total = tempStudents.Count, Success = success, Errors = errors });
        }
    }
}
