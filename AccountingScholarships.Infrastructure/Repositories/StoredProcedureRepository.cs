using AccountingScholarships.Application.Common;
using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AccountingScholarships.Infrastructure.Repositories;

/// <summary>
/// Вызов хранимых процедур через EpvoSsoDbContext (EPVO_test).
/// </summary>
public class StoredProcedureRepository : IStoredProcedureRepository
{
    private readonly EpvoSsoDbContext _context;
    private readonly IComparisonRepository _comparisonRepo;
    private readonly string _dataSource;

    public StoredProcedureRepository(EpvoSsoDbContext context, IComparisonRepository comparisonRepo, Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        _context = context;
        _comparisonRepo = comparisonRepo;
        _dataSource = configuration["SyncSettings:EpvoDataSource"] ?? "Dump";
    }

    public async Task<StoredProcedureResult> ExecuteReloadStudentAsync(CancellationToken ct = default)
    {
        // 1. Запоминаем ID-шки студентов ДО выполнения процедуры
        var existingIds = await _context.Student_Sso
            .AsNoTracking()
            .Select(s => s.StudentId)
            .ToListAsync(ct);

        var existingIdSet = new HashSet<int>(existingIds);

        // 2. Выполняем хранимую процедуру
        var returnValueParam = new SqlParameter
        {
            ParameterName = "@ReturnValue",
            SqlDbType = System.Data.SqlDbType.Int,
            Direction = System.Data.ParameterDirection.Output
        };

        var rowsAffected = await _context.Database
            .ExecuteSqlRawAsync(
                "EXEC @ReturnValue = [dbo].[Reload_STUDENT]",
                new[] { returnValueParam },
                ct);

        var returnValue = (int)returnValueParam.Value;

        // 3. Забираем НОВЫЕ записи (которых не было до выполнения)
        var insertedStudents = await _context.Student_Sso
            .AsNoTracking()
            .Where(s => !existingIdSet.Contains(s.StudentId))
            .ToListAsync(ct);

        return new StoredProcedureResult
        {
            ReturnValue = returnValue,
            RowsAffected = rowsAffected,
            ExecutedAt = DateTime.UtcNow,
            Message = returnValue == 0
                ? $"Успешно выполнено. Обработано записей: {rowsAffected}. Новых студентов: {insertedStudents.Count}."
                : $"Ошибка выполнения. Код возврата: {returnValue}.",
            InsertedStudents = insertedStudents
        };
    }

    public async Task<List<Dictionary<string, object?>>> ReadReloadStudentAsync(CancellationToken ct = default)
    {
        var results = new List<Dictionary<string, object?>>();

        var connection = _context.Database.GetDbConnection();
        await connection.OpenAsync(ct);

        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = "EXEC [dbo].[Reload_STUDENT]";
            command.CommandTimeout = 120;

            using var reader = await command.ExecuteReaderAsync(ct);

            while (await reader.ReadAsync(ct))
            {
                var row = new Dictionary<string, object?>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }
                results.Add(row);
            }
        }
        finally
        {
            await connection.CloseAsync();
        }

        return results;
    }

    public async Task<int> SaveReloadStudentToTempAsync(CancellationToken ct = default)
    {
        // 1. Читаем результат SP
        var rows = await ReadReloadStudentAsync(ct);

        // 2. Маппим в сущности Student_Temp
        var entities = rows.Select(MapRowToStudentTemp).ToList();

        // 3. Очищаем только системные записи (без SyncSessionId), не трогаем ручные сессии
        await _context.Database.ExecuteSqlRawAsync(
            "DELETE FROM [dbo].[STUDENT_TEMP] WHERE SyncSessionId IS NULL", ct);

        // 4. Вставляем записи
        await _context.Student_Temp.AddRangeAsync(entities, ct);
        await _context.SaveChangesAsync(ct);

        return entities.Count;
    }

    public async Task<SendTempResult> SendTempToEpvoAsync(string triggeredBy, CancellationToken ct = default)
    {
        // 1. Читаем все записи из STUDENT_TEMP
        var tempStudents = await _context.Student_Temp
            .AsNoTracking()
            .ToListAsync(ct);

        if (tempStudents.Count == 0)
        {
            return new SendTempResult
            {
                Total = 0, Success = 0, Errors = 0,
                Message = "STUDENT_TEMP пуст — нечего синхронизировать."
            };
        }

        int success = 0;
        int errors = 0;
        var logs = new List<StudentSyncLog>();

        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var useSso = _dataSource.Equals("Sso", StringComparison.OrdinalIgnoreCase);
        var targetTable = useSso ? "STUDENT_SSO" : "STUDENT_DUMP";

        // 2. Загружаем существующие записи из целевой таблицы в память и строим словарь
        var tempIds = new HashSet<int>(tempStudents.Select(s => s.StudentId));
        Dictionary<int, Student_Dump> existingDumps = new();
        Dictionary<int, Student_Sso> existingSso = new();

        if (useSso)
        {
            var allSso = await _context.Student_Sso.AsNoTracking().ToListAsync(ct);
            existingSso = allSso.Where(d => tempIds.Contains(d.StudentId)).ToDictionary(d => d.StudentId);
        }
        else
        {
            var allDumps = await _context.Student_Dumps.AsNoTracking().ToListAsync(ct);
            existingDumps = allDumps.Where(d => tempIds.Contains(d.StudentId)).ToDictionary(d => d.StudentId);
        }

        // 3. UPSERT: обновляем существующие, добавляем новые
        foreach (var temp in tempStudents)
        {
            var log = new StudentSyncLog
            {
                StudentId    = temp.StudentId,
                IinPlt       = temp.IinPlt,
                SentAt       = DateTime.UtcNow,
                EpvoEndpoint = $"{targetTable} (local sync)",
                RequestBody  = JsonSerializer.Serialize(temp, jsonOptions),
                TriggeredBy  = triggeredBy
            };

            try
            {
                if (useSso)
                {
                    if (existingSso.TryGetValue(temp.StudentId, out var existing))
                    {
                        CopyTempToTarget(temp, existing);
                    }
                    else
                    {
                        var newSso = new Student_Sso();
                        CopyTempToTarget(temp, newSso);
                        _context.Student_Sso.Add(newSso);
                    }
                }
                else
                {
                    if (existingDumps.TryGetValue(temp.StudentId, out var existing))
                    {
                        CopyTempToTarget(temp, existing);
                    }
                    else
                    {
                        var newDump = new Student_Dump();
                        CopyTempToTarget(temp, newDump);
                        _context.Student_Dumps.Add(newDump);
                    }
                }

                log.Status       = "Success";
                log.ResponseBody = $"{{\"result\":\"synced_to_{targetTable.ToLowerInvariant()}\"}}";
                success++;
            }
            catch (Exception ex)
            {
                log.Status       = "Error";
                log.ErrorMessage = ex.Message;
                errors++;
            }

            logs.Add(log);
        }

        // 4. Сохраняем изменения в целевой таблице и логи одним батчем
        await _context.StudentSyncLogs.AddRangeAsync(logs, ct);
        await _context.SaveChangesAsync(ct);

        return new SendTempResult
        {
            Total   = tempStudents.Count,
            Success = success,
            Errors  = errors,
            Message = $"{targetTable} обновлён. Обработано: {tempStudents.Count}. Успешно: {success}. Ошибок: {errors}."
        };
    }

    #region Mapping Helpers

    private static EpvoStudentTempDto MapRowToEpvoStudentTempDto(Dictionary<string, object?> row)
    {
        return new EpvoStudentTempDto
        {
            UniversityId = GetInt(row, "universityId"),
            StudentId = GetInt(row, "studentId") ?? 0,
            FirstName = GetStr(row, "firstname"),
            LastName = GetStr(row, "lastname"),
            Patronymic = GetStr(row, "patronymic"),
            BirthDate = GetDate(row, "birthDate"),
            StartDate = GetDate(row, "startDate"),
            Address = GetStr(row, "address"),
            NationId = GetInt(row, "nationid"),
            StudyFormId = GetInt(row, "studyformid"),
            PaymentFormId = GetInt(row, "paymentformid"),
            StudyLanguageId = GetInt(row, "studylanguageid"),
            Photo = null,
            ProfessionId = GetInt(row, "professionid"),
            CourseNumber = GetInt(row, "coursenumber"),
            TranscriptNumber = GetStr(row, "transcriptNumber"),
            TranscriptSeries = GetStr(row, "transcriptSeries"),
            IsMarried = GetInt(row, "ismarried"),
            IcNumber = GetStr(row, "icnumber"),
            IcDate = GetDate(row, "icDate"),
            Education = GetStr(row, "education"),
            HasExcellent = GetBool(row, "hasexcellent"),
            StartOrder = GetStr(row, "startorder"),
            IsStudent = GetInt(row, "isstudent"),
            Certificate = GetStr(row, "certificate"),
            GrantNumber = GetStr(row, "grantnumber"),
            Gpa = GetDecimal(row, "gpa"),
            CurrentCreditsSum = GetFloat(row, "currentCreditsSum"),
            Residence = GetInt(row, "residence"),
            SitizenshipId = GetInt(row, "sitizenshipid"),
            DormState = GetInt(row, "dormState"),
            IsInRetire = GetBool(row, "isinretire"),
            FromId = GetInt(row, "fromid"),
            Local = GetBool(row, "local"),
            City = GetStr(row, "city"),
            ContractId = GetInt(row, "contractid"),
            SpecializationId = GetInt(row, "specializationid"),
            IinPlt = GetStr(row, "iinplt"),
            AltynBelgi = GetBool(row, "altynBelgi"),
            DataVydachiAttestata = GetDate(row, "datavydachiattestata"),
            DataVydachiDiploma = GetDate(row, "datavydachidiploma"),
            DateDocEducation = GetDate(row, "dateDocEducation"),
            EndCollege = GetBool(row, "endCollege"),
            EndHighSchool = GetBool(row, "endHighSchool"),
            EndSchool = GetBool(row, "endSchool"),
            IcSeries = GetStr(row, "icseries"),
            IcType = GetInt(row, "ictype"),
            LivingAddress = GetStr(row, "livingAddress"),
            NomerAttestata = GetStr(row, "nomerattestata"),
            OtherBirthPlace = GetStr(row, "otherBirthPlace"),
            SeriesNumberDocEducation = GetStr(row, "seriesNumberDocEducation"),
            SeriyaAttestata = GetStr(row, "seriyaattestata"),
            SeriyaDiploma = GetStr(row, "seriyaDiploma"),
            SchoolName = GetStr(row, "schoolName"),
            FacultyId = GetInt(row, "facultyId"),
            SexId = GetInt(row, "sexid"),
            Mail = GetStr(row, "mail"),
            Phone = GetStr(row, "phone"),
            SumPoints = GetInt(row, "sumPoints"),
            SumPointsCreative = GetInt(row, "sumPointsCreative"),
            EnrollOrderDate = GetDate(row, "enrollOrderDate"),
            MobilePhone = GetStr(row, "mobilePhone"),
            GrantType = GetInt(row, "grant_type"),
            AcademicMobility = GetInt(row, "academicMobility"),
            IncorrectIin = GetBool(row, "incorrectIin"),
            BirthPlaceCatoId = GetInt(row, "birthPlaceCatoId"),
            LivingPlaceCatoId = GetInt(row, "livingPlaceCatoId"),
            RegistrationPlaceCatoId = GetInt(row, "registrationPlaceCatoId"),
            NaselennyiPunktAttestataCatoId = GetInt(row, "naselennyiPunktAttestataCatoId"),
            FundingId = GetInt(row, "fundingId"),
            TypeCode = GetStr(row, "typeCode")
        };
    }

    private static void CopyTempToTarget(Student_Temp src, object dst)
    {
        var srcProps = typeof(Student_Temp).GetProperties();
        var dstType = dst.GetType();
        foreach (var sp in srcProps)
        {
            var dp = dstType.GetProperty(sp.Name);
            if (dp != null && dp.CanWrite)
            {
                dp.SetValue(dst, sp.GetValue(src));
            }
        }
    }

    private static Student_Temp MapDtoToEntity(EpvoStudentTempDto dto)
    {
        return new Student_Temp
        {
            UniversityId = dto.UniversityId,
            StudentId = dto.StudentId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Patronymic = dto.Patronymic,
            BirthDate = dto.BirthDate,
            StartDate = dto.StartDate,
            Address = dto.Address,
            NationId = dto.NationId,
            StudyFormId = dto.StudyFormId,
            StudyCalendarId = dto.StudyCalendarId,
            PaymentFormId = dto.PaymentFormId,
            StudyLanguageId = dto.StudyLanguageId,
            Photo = dto.Photo,
            ProfessionId = dto.ProfessionId,
            CourseNumber = dto.CourseNumber,
            TranscriptNumber = dto.TranscriptNumber,
            TranscriptSeries = dto.TranscriptSeries,
            IsMarried = dto.IsMarried,
            IcNumber = dto.IcNumber,
            IcDate = dto.IcDate,
            Education = dto.Education,
            HasExcellent = dto.HasExcellent,
            StartOrder = dto.StartOrder,
            IsStudent = dto.IsStudent,
            Certificate = dto.Certificate,
            GrantNumber = dto.GrantNumber,
            Gpa = dto.Gpa,
            CurrentCreditsSum = dto.CurrentCreditsSum,
            Residence = dto.Residence,
            SitizenshipId = dto.SitizenshipId,
            DormState = dto.DormState,
            IsInRetire = dto.IsInRetire,
            FromId = dto.FromId,
            Local = dto.Local,
            City = dto.City,
            ContractId = dto.ContractId,
            SpecializationId = dto.SpecializationId,
            IinPlt = dto.IinPlt,
            AltynBelgi = dto.AltynBelgi,
            DataVydachiAttestata = dto.DataVydachiAttestata,
            DataVydachiDiploma = dto.DataVydachiDiploma,
            DateDocEducation = dto.DateDocEducation,
            EndCollege = dto.EndCollege,
            EndHighSchool = dto.EndHighSchool,
            EndSchool = dto.EndSchool,
            IcSeries = dto.IcSeries,
            IcType = dto.IcType,
            LivingAddress = dto.LivingAddress,
            NomerAttestata = dto.NomerAttestata,
            OtherBirthPlace = dto.OtherBirthPlace,
            SeriesNumberDocEducation = dto.SeriesNumberDocEducation,
            SeriyaAttestata = dto.SeriyaAttestata,
            SeriyaDiploma = dto.SeriyaDiploma,
            SchoolName = dto.SchoolName,
            FacultyId = dto.FacultyId,
            SexId = dto.SexId,
            Mail = dto.Mail,
            Phone = dto.Phone,
            SumPoints = dto.SumPoints,
            SumPointsCreative = dto.SumPointsCreative,
            EnrollOrderDate = dto.EnrollOrderDate,
            MobilePhone = dto.MobilePhone,
            GrantType = dto.GrantType,
            AcademicMobility = dto.AcademicMobility,
            IncorrectIin = dto.IncorrectIin,
            BirthPlaceCatoId = dto.BirthPlaceCatoId,
            LivingPlaceCatoId = dto.LivingPlaceCatoId,
            RegistrationPlaceCatoId = dto.RegistrationPlaceCatoId,
            NaselennyiPunktAttestataCatoId = dto.NaselennyiPunktAttestataCatoId,
            EnterExamType = dto.EnterExamType,
            FundingId = dto.FundingId,
            TypeCode = dto.TypeCode
        };
    }

    private static Student_Temp MapRowToStudentTemp(Dictionary<string, object?> row)
    {
        return new Student_Temp
        {
            UniversityId          = GetInt(row, "universityId"),
            StudentId             = GetInt(row, "studentId") ?? 0,
            FirstName             = GetStr(row, "firstname"),
            LastName              = GetStr(row, "lastname"),
            Patronymic            = GetStr(row, "patronymic"),
            BirthDate             = GetDate(row, "birthDate"),
            StartDate             = GetDate(row, "startDate"),
            Address               = GetStr(row, "address"),
            NationId              = GetInt(row, "nationid"),
            StudyFormId           = GetInt(row, "studyformid"),
            PaymentFormId         = GetInt(row, "paymentformid"),
            StudyLanguageId       = GetInt(row, "studylanguageid"),
            Photo                 = null,
            ProfessionId          = GetInt(row, "professionid"),
            CourseNumber          = GetInt(row, "coursenumber"),
            TranscriptNumber      = GetStr(row, "transcriptNumber"),
            TranscriptSeries      = GetStr(row, "transcriptSeries"),
            IsMarried             = GetInt(row, "ismarried"),
            IcNumber              = GetStr(row, "icnumber"),
            IcDate                = GetDate(row, "icDate"),
            Education             = GetStr(row, "education"),
            HasExcellent          = GetBool(row, "hasexcellent"),
            StartOrder            = GetStr(row, "startorder"),
            IsStudent             = GetInt(row, "isstudent"),
            Certificate           = GetStr(row, "certificate"),
            GrantNumber           = GetStr(row, "grantnumber"),
            Gpa                   = GetDecimal(row, "gpa"),
            CurrentCreditsSum     = GetFloat(row, "currentCreditsSum"),
            Residence             = GetInt(row, "residence"),
            SitizenshipId         = GetInt(row, "sitizenshipid"),
            DormState             = GetInt(row, "dormState"),
            IsInRetire            = GetBool(row, "isinretire"),
            FromId                = GetInt(row, "fromid"),
            Local                 = GetBool(row, "local"),
            City                  = GetStr(row, "city"),
            ContractId            = GetInt(row, "contractid"),
            SpecializationId      = GetInt(row, "specializationid"),
            IinPlt                = GetStr(row, "iinplt"),
            AltynBelgi            = GetBool(row, "altynBelgi"),
            DataVydachiAttestata  = GetDate(row, "datavydachiattestata"),
            DataVydachiDiploma    = GetDate(row, "datavydachidiploma"),
            DateDocEducation      = GetDate(row, "dateDocEducation"),
            EndCollege            = GetBool(row, "endCollege"),
            EndHighSchool         = GetBool(row, "endHighSchool"),
            EndSchool             = GetBool(row, "endSchool"),
            IcSeries              = GetStr(row, "icseries"),
            IcType                = GetInt(row, "ictype"),
            LivingAddress         = GetStr(row, "livingAddress"),
            NomerAttestata        = GetStr(row, "nomerattestata"),
            OtherBirthPlace       = GetStr(row, "otherBirthPlace"),
            SeriesNumberDocEducation = GetStr(row, "seriesNumberDocEducation"),
            SeriyaAttestata       = GetStr(row, "seriyaattestata"),
            SeriyaDiploma         = GetStr(row, "seriyaDiploma"),
            SchoolName            = GetStr(row, "schoolName"),
            FacultyId             = GetInt(row, "facultyId"),
            SexId                 = GetInt(row, "sexid"),
            Mail                  = GetStr(row, "mail"),
            Phone                 = GetStr(row, "phone"),
            SumPoints             = GetInt(row, "sumPoints"),
            SumPointsCreative     = GetInt(row, "sumPointsCreative"),
            EnrollOrderDate       = GetDate(row, "enrollOrderDate"),
            MobilePhone           = GetStr(row, "mobilePhone"),
            GrantType             = GetInt(row, "grant_type"),
            AcademicMobility      = GetInt(row, "academicMobility"),
            IncorrectIin          = GetBool(row, "incorrectIin"),
            BirthPlaceCatoId      = GetInt(row, "birthPlaceCatoId"),
            LivingPlaceCatoId     = GetInt(row, "livingPlaceCatoId"),
            RegistrationPlaceCatoId = GetInt(row, "registrationPlaceCatoId"),
            NaselennyiPunktAttestataCatoId = GetInt(row, "naselennyiPunktAttestataCatoId"),
            FundingId             = GetInt(row, "fundingId"),
            TypeCode              = GetStr(row, "typeCode"),
        };
    }

    private static int? GetInt(Dictionary<string, object?> row, string key)
    {
        var val = GetRaw(row, key);
        if (val == null) return null;
        try { return Convert.ToInt32(val); } catch { return null; }
    }

    private static string? GetStr(Dictionary<string, object?> row, string key)
    {
        var val = GetRaw(row, key);
        return val?.ToString();
    }

    private static decimal? GetDecimal(Dictionary<string, object?> row, string key)
    {
        var val = GetRaw(row, key);
        if (val == null) return null;
        try { return Convert.ToDecimal(val); } catch { return null; }
    }

    private static float? GetFloat(Dictionary<string, object?> row, string key)
    {
        var val = GetRaw(row, key);
        if (val == null) return null;
        try { return Convert.ToSingle(val); } catch { return null; }
    }

    private static bool? GetBool(Dictionary<string, object?> row, string key)
    {
        var val = GetRaw(row, key);
        if (val == null) return null;
        if (val is bool b) return b;
        if (val is int i) return i != 0;
        if (val is byte by) return by != 0;
        try { return Convert.ToBoolean(val); } catch { return null; }
    }

    private static DateOnly? GetDate(Dictionary<string, object?> row, string key)
    {
        var val = GetRaw(row, key);
        if (val == null) return null;
        if (val is DateOnly d) return d;
        if (val is DateTime dt) return DateOnly.FromDateTime(dt);
        if (DateTime.TryParse(val.ToString(), out var parsed)) return DateOnly.FromDateTime(parsed);
        return null;
    }

    private static object? GetRaw(Dictionary<string, object?> row, string key)
    {
        var entry = row.FirstOrDefault(kv =>
            string.Equals(kv.Key, key, StringComparison.OrdinalIgnoreCase));
        var val = entry.Value;
        if (val == null || val is DBNull) return null;
        return val;
    }

    #endregion

    public async Task<int> SavePreviewToTempAsync(List<EpvoStudentTempDto> items, CancellationToken ct = default)
    {
        if (items == null || items.Count == 0) return 0;

        await _context.Database.ExecuteSqlRawAsync(
            "DELETE FROM [dbo].[STUDENT_TEMP] WHERE SyncSessionId IS NULL", ct);

        var entities = items.Select(MapDtoToEntity).ToList();

        await _context.Student_Temp.AddRangeAsync(entities, ct);
        await _context.SaveChangesAsync(ct);

        return entities.Count;
    }

    public async Task UpsertStudentTempAsync(EpvoStudentTempDto dto, CancellationToken ct = default)
    {
        var existing = await _context.Student_Temp.FindAsync(new object[] { dto.StudentId }, ct);
        if (existing != null)
        {
            var updated = MapDtoToEntity(dto);
            _context.Entry(existing).CurrentValues.SetValues(updated);
        }
        else
        {
            _context.Student_Temp.Add(MapDtoToEntity(dto));
        }

        await _context.SaveChangesAsync(ct);
    }

    public async Task<SyncLogPagedResult> GetSyncLogsAsync(string? status, int page, int pageSize, CancellationToken ct = default)
    {
        var query = _context.StudentSyncLogs.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(x => x.Status == status);

        var total = await query.CountAsync(ct);

        var logs = await query
            .OrderByDescending(x => x.SentAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new SyncLogPagedResult
        {
            Total = total,
            Page = page,
            PageSize = pageSize,
            Logs = logs
        };
    }

    public async Task<IReadOnlyList<StudentSyncLog>> GetSyncLogsByStudentAsync(int studentId, CancellationToken ct = default)
    {
        return await _context.StudentSyncLogs
            .AsNoTracking()
            .Where(x => x.StudentId == studentId)
            .OrderByDescending(x => x.SentAt)
            .ToListAsync(ct);
    }
}
