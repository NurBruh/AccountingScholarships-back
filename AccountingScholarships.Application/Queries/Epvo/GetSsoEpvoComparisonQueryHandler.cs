using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.Epvo;

public class GetSsoEpvoComparisonQueryHandler : IRequestHandler<GetSsoEpvoComparisonQuery, SsoEpvoComparisonDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEpvoRepository _epvoRepository;

    public GetSsoEpvoComparisonQueryHandler(IUnitOfWork unitOfWork, IEpvoRepository epvoRepository)
    {
        _unitOfWork = unitOfWork;
        _epvoRepository = epvoRepository;
    }

    public async Task<SsoEpvoComparisonDto> Handle(GetSsoEpvoComparisonQuery request, CancellationToken cancellationToken)
    {
        var ssoStudents = await _unitOfWork.Students.GetAllWithDetailsAsync(cancellationToken);
        var epvoStudents = await _epvoRepository.GetAllAsync(cancellationToken);

        var epvoMap = epvoStudents.ToDictionary(e => e.IIN);
        var ssoIINs = new HashSet<string>(ssoStudents.Select(s => s.IIN));

        var items = new List<SsoEpvoComparisonItemDto>();

        foreach (var sso in ssoStudents)
        {
            var activeGrant = sso.Grants?.FirstOrDefault(g => g.IsActive);
            var activeScholarship = sso.Scholarships?.FirstOrDefault(s => s.IsActive);

            var ssoData = new StudentSsoDataDto
            {
                Id = sso.Id,
                FirstName = sso.FirstName,
                LastName = sso.LastName,
                MiddleName = sso.MiddleName,
                IIN = sso.IIN,
                Faculty = sso.Faculty,
                Speciality = sso.Speciality,
                Course = sso.Course,
                GrantName = activeGrant?.Name,
                GrantAmount = activeGrant?.Amount,
                ScholarshipName = activeScholarship?.Name,
                ScholarshipAmount = activeScholarship?.Amount,
                Iban = sso.iban,
                IsActive = sso.IsActive
            };

            if (!epvoMap.TryGetValue(sso.IIN, out var epvo))
            {
                items.Add(new SsoEpvoComparisonItemDto
                {
                    IIN = sso.IIN,
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
                Iban = epvo.iban,
                IsActive = epvo.IsActive,
                SyncDate = epvo.SyncDate
            };

            var differences = DetectDifferences(ssoData, epvoData);
            items.Add(new SsoEpvoComparisonItemDto
            {
                IIN = sso.IIN,
                SsoData = ssoData,
                EpvoData = epvoData,
                Differences = differences,
                HasDifferences = differences.Count > 0
            });
        }

        // EPVO students not present in SSO
        foreach (var epvo in epvoStudents)
        {
            if (!ssoIINs.Contains(epvo.IIN))
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
        Check("iban", "IBAN", sso.Iban, epvo.Iban);
        Check("isActive", "Активен", sso.IsActive.ToString(), epvo.IsActive.ToString());

        return diffs;
    }
}
