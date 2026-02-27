using AccountingScholarships.Domain;
using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.Epvo;

public class GetSsoEpvoComparisonQueryHandler : IRequestHandler<GetSsoEpvoComparisonQuery, SsoEpvoComparisonDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEpvoRepository _epvoRepository;
    private readonly IPosrednikRepository _posrednikRepository;

    public GetSsoEpvoComparisonQueryHandler(
        IUnitOfWork unitOfWork,
        IEpvoRepository epvoRepository,
        IPosrednikRepository posrednikRepository)
    {
        _unitOfWork = unitOfWork;
        _epvoRepository = epvoRepository;
        _posrednikRepository = posrednikRepository;
    }

    public async Task<SsoEpvoComparisonDto> Handle(GetSsoEpvoComparisonQuery request, CancellationToken cancellationToken)
    {
        // 1. Обновляем Посредник из ССО
        await RefreshPosrednikFromSso(cancellationToken);

        // 2. Загружаем данные из Посредника и ЕПВО
        var posrednikStudents = await _posrednikRepository.GetAllAsync(cancellationToken);
        var epvoStudents = await _epvoRepository.GetAllAsync(cancellationToken);

        var epvoMap = epvoStudents.ToDictionary(e => e.IIN);
        var posrednikIINs = new HashSet<string>(posrednikStudents.Select(s => s.IIN));

        var items = new List<SsoEpvoComparisonItemDto>();

        // 3. Сравниваем Посредник (ССО) vs ЕПВО
        foreach (var p in posrednikStudents)
        {
            var ssoData = new StudentSsoDataDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                MiddleName = p.MiddleName,
                IIN = p.IIN,
                Faculty = p.Faculty,
                Speciality = p.Speciality,
                Course = p.Course,
                GrantName = p.GrantName,
                GrantAmount = p.GrantAmount,
                ScholarshipName = p.ScholarshipName,
                ScholarshipAmount = p.ScholarshipAmount,
                ScholarshipNotes = p.ScholarshipNotes,
                Iban = p.iban,
                IsActive = p.IsActive
            };

            if (!epvoMap.TryGetValue(p.IIN, out var epvo))
            {
                items.Add(new SsoEpvoComparisonItemDto
                {
                    IIN = p.IIN,
                    SsoData = ssoData,
                    EpvoData = null,
                    OnlyInSso = true,
                    Differences = new List<FieldDifferenceDto>()
                });
                continue;
            }

            var epvoData = new StudentEpvoDataDto
            {
                Id = epvo.Id,
                FirstName = epvo.FirstName,
                LastName = epvo.LastName,
                MiddleName = epvo.MiddleName,
                IIN = epvo.IIN,
                Faculty = epvo.Faculty,
                Speciality = epvo.Speciality,
                Course = epvo.Course,
                GrantName = epvo.GrantName,
                GrantAmount = epvo.GrantAmount,
                ScholarshipName = epvo.ScholarshipName,
                ScholarshipAmount = epvo.ScholarshipAmount,
                ScholarshipNotes = epvo.ScholarshipNotes,
                Iban = epvo.iban,
                IsActive = epvo.IsActive,
                SyncDate = epvo.SyncDate
            };

            var differences = DetectDifferences(ssoData, epvoData);
            items.Add(new SsoEpvoComparisonItemDto
            {
                IIN = p.IIN,
                SsoData = ssoData,
                EpvoData = epvoData,
                Differences = differences,
                HasDifferences = differences.Count > 0
            });
        }

        // Студенты только в ЕПВО
        foreach (var epvo in epvoStudents)
        {
            if (!posrednikIINs.Contains(epvo.IIN))
            {
                items.Add(new SsoEpvoComparisonItemDto
                {
                    IIN = epvo.IIN,
                    SsoData = null,
                    EpvoData = new StudentEpvoDataDto
                    {
                        Id = epvo.Id,
                        FirstName = epvo.FirstName,
                        LastName = epvo.LastName,
                        MiddleName = epvo.MiddleName,
                        IIN = epvo.IIN,
                        Faculty = epvo.Faculty,
                        Speciality = epvo.Speciality,
                        Course = epvo.Course,
                        GrantName = epvo.GrantName,
                        GrantAmount = epvo.GrantAmount,
                        ScholarshipName = epvo.ScholarshipName,
                        ScholarshipAmount = epvo.ScholarshipAmount,
                        Iban = epvo.iban,
                        IsActive = epvo.IsActive,
                        SyncDate = epvo.SyncDate
                    },
                    OnlyInEpvo = true,
                    Differences = new List<FieldDifferenceDto>()
                });
            }
        }

        return new SsoEpvoComparisonDto
        {
            Items = items,
            TotalDifferences = items.Count(i => i.HasDifferences || i.OnlyInSso || i.OnlyInEpvo),
            OnlyInSso = items.Count(i => i.OnlyInSso),
            OnlyInEpvo = items.Count(i => i.OnlyInEpvo)
        };
    }

    /// <summary>
    /// Обновляет таблицу-посредник данными из ССО (Student + Grants + Scholarships).
    /// Оптимизировано: предзагрузка всех posrednik записей одним запросом.
    /// </summary>
    private async Task RefreshPosrednikFromSso(CancellationToken cancellationToken)
    {
        var ssoStudents = await _unitOfWork.Students.GetAllWithDetailsAsync(cancellationToken);

        // Предзагрузка ВСЕХ записей посредника одним запросом (вместо N+1)
        var posrednikMap = await _posrednikRepository.GetAllAsDictionaryByIINAsync(cancellationToken);

        foreach (var sso in ssoStudents)
        {
            var activeGrant = sso.Grants?.FirstOrDefault(g => g.IsActive);
            var activeScholarship = sso.Scholarships?.FirstOrDefault(s => s.IsActive);
            var latestScholarship = sso.Scholarships?.OrderByDescending(s => s.CreatedAt).FirstOrDefault();

            if (!posrednikMap.TryGetValue(sso.IIN, out var existing))
            {
                var posrednik = new EpvoPosrednik
                {
                    FirstName = sso.FirstName,
                    LastName = sso.LastName,
                    MiddleName = sso.MiddleName,
                    IIN = sso.IIN,
                    DateOfBirth = sso.DateOfBirth,
                    Faculty = sso.Faculty,
                    Speciality = sso.Speciality,
                    Course = sso.Course,
                    GrantName = activeGrant?.Name,
                    GrantAmount = activeGrant?.Amount,
                    ScholarshipName = activeScholarship?.Name,
                    ScholarshipAmount = activeScholarship?.Amount,
                    ScholarshipLostDate = latestScholarship?.LostDate,
                    ScholarshipOrderLostDate = latestScholarship?.OrderLostDate,
                    ScholarshipOrderCandidateDate = latestScholarship?.OrderCandidateDate,
                    ScholarshipNotes = latestScholarship?.Notes,
                    iban = sso.iban,
                    IsActive = sso.IsActive,
                    SyncDate = DateTime.UtcNow
                };
                await _posrednikRepository.AddAsync(posrednik, cancellationToken);
            }
            else
            {
                existing.FirstName = sso.FirstName;
                existing.LastName = sso.LastName;
                existing.MiddleName = sso.MiddleName;
                existing.DateOfBirth = sso.DateOfBirth;
                existing.Faculty = sso.Faculty;
                existing.Speciality = sso.Speciality;
                existing.Course = sso.Course;
                existing.GrantName = activeGrant?.Name;
                existing.GrantAmount = activeGrant?.Amount;
                existing.ScholarshipName = activeScholarship?.Name;
                existing.ScholarshipAmount = activeScholarship?.Amount;
                existing.ScholarshipLostDate = latestScholarship?.LostDate;
                existing.ScholarshipOrderLostDate = latestScholarship?.OrderLostDate;
                existing.ScholarshipOrderCandidateDate = latestScholarship?.OrderCandidateDate;
                existing.ScholarshipNotes = latestScholarship?.Notes;
                existing.iban = sso.iban;
                existing.IsActive = sso.IsActive;
                existing.SyncDate = DateTime.UtcNow;
                await _posrednikRepository.UpdateAsync(existing, cancellationToken);
            }
        }

        // Один SaveChanges в конце вместо отдельного на каждую запись
        await _posrednikRepository.SaveChangesAsync(cancellationToken);
    }

    private static List<FieldDifferenceDto> DetectDifferences(StudentSsoDataDto sso, StudentEpvoDataDto epvo)
    {
        var diffs = new List<FieldDifferenceDto>();

        void Check(string field, string label, string? ssoVal, string? epvoVal)
        {
            if ((ssoVal ?? "") != (epvoVal ?? ""))
                diffs.Add(new FieldDifferenceDto { Field = field, FieldLabel = label, SsoValue = ssoVal, EpvoValue = epvoVal });
        }

        Check("firstName", "Имя", sso.FirstName, epvo.FirstName);
        Check("lastName", "Фамилия", sso.LastName, epvo.LastName);
        Check("middleName", "Отчество", sso.MiddleName, epvo.MiddleName);
        Check("faculty", "Институт", sso.Faculty, epvo.Faculty);
        Check("speciality", "Специальность", sso.Speciality, epvo.Speciality);
        Check("course", "Курс", sso.Course.ToString(), epvo.Course.ToString());
        Check("grantName", "Тип гранта", sso.GrantName, epvo.GrantName);
        Check("grantAmount", "Сумма гранта", sso.GrantAmount?.ToString("F2"), epvo.GrantAmount?.ToString("F2"));
        Check("scholarshipName", "Стипендия", sso.ScholarshipName, epvo.ScholarshipName);
        Check("scholarshipAmount", "Сумма стипендии", sso.ScholarshipAmount?.ToString("F2"), epvo.ScholarshipAmount?.ToString("F2"));
        Check("scholarshipNotes", "Примечания стипендии", sso.ScholarshipNotes, epvo.ScholarshipNotes);
        Check("iban", "IBAN", sso.Iban, epvo.Iban);
        Check("isActive", "Активен", sso.IsActive.ToString(), epvo.IsActive.ToString());

        return diffs;
    }
}
