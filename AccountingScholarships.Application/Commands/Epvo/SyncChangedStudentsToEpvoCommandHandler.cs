using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Entities;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

/// <summary>
/// Обработчик: собирает изменённых студентов (SSO vs ЕПВО напрямую),
/// формирует массив и одним POST отправляет в ЕПВО через IEpvoApiClient.
/// </summary>
public class SyncChangedStudentsToEpvoCommandHandler : IRequestHandler<SyncChangedStudentsToEpvoCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEpvoRepository _epvoRepository;
    private readonly IEpvoApiClient _epvoApiClient;

    public SyncChangedStudentsToEpvoCommandHandler(
        IUnitOfWork unitOfWork,
        IEpvoRepository epvoRepository,
        IEpvoApiClient epvoApiClient)
    {
        _unitOfWork = unitOfWork;
        _epvoRepository = epvoRepository;
        _epvoApiClient = epvoApiClient;
    }

    public async Task<int> Handle(SyncChangedStudentsToEpvoCommand request, CancellationToken cancellationToken)
    {
        var ssoStudents = await _unitOfWork.Students.GetAllWithDetailsAsync(cancellationToken);
        var epvoMap = await _epvoRepository.GetAllAsDictionaryByIINAsync(cancellationToken);

        var changedPayload = new List<EpvoSendPayloadDto>();

        foreach (var sso in ssoStudents)
        {
            var payload = MapStudentToPayload(sso);

            if (!epvoMap.TryGetValue(sso.IIN, out var epvo))
            {
                changedPayload.Add(payload);
                continue;
            }

            if (HasDifferences(sso, epvo))
            {
                changedPayload.Add(payload);
            }
        }

        if (changedPayload.Count == 0)
            return 0;

        await _epvoApiClient.SendStudentsAsync(changedPayload, cancellationToken);

        return changedPayload.Count;
    }

    private static bool HasDifferences(Student sso, Domain.Entities.Epvo.EpvoStudent epvo)
    {
        var faculty = sso.Speciality?.Department?.Institute?.InstituteName;
        var speciality = sso.Speciality?.SpecialityName;
        var activeGrant = sso.Grants?.FirstOrDefault(g => g.IsActive);
        var activeScholarship = sso.Scholarships?.FirstOrDefault(s => s.IsActive);
        var latestScholarship = sso.Scholarships?.OrderByDescending(s => s.CreatedAt).FirstOrDefault();

        return sso.FirstName != epvo.FirstName
            || sso.LastName != epvo.LastName
            || sso.MiddleName != epvo.MiddleName
            || faculty != epvo.Faculty
            || speciality != epvo.Speciality
            || sso.Course != epvo.Course
            || activeGrant?.Name != epvo.GrantName
            || activeGrant?.Amount != epvo.GrantAmount
            || activeScholarship?.Name != epvo.ScholarshipName
            || activeScholarship?.Amount != epvo.ScholarshipAmount
            || latestScholarship?.LostDate != epvo.ScholarshipLostDate
            || latestScholarship?.OrderLostDate != epvo.ScholarshipOrderLostDate
            || latestScholarship?.OrderCandidateDate != epvo.ScholarshipOrderCandidateDate
            || latestScholarship?.Notes != epvo.ScholarshipNotes
            || sso.iban != epvo.iban
            || sso.IsActive != epvo.IsActive;
    }

    private static EpvoSendPayloadDto MapStudentToPayload(Student sso)
    {
        var activeGrant = sso.Grants?.FirstOrDefault(g => g.IsActive);
        var activeScholarship = sso.Scholarships?.FirstOrDefault(s => s.IsActive);
        var latestScholarship = sso.Scholarships?.OrderByDescending(s => s.CreatedAt).FirstOrDefault();

        return new EpvoSendPayloadDto
        {
            IIN = sso.IIN,
            FirstName = sso.FirstName,
            LastName = sso.LastName,
            MiddleName = sso.MiddleName,
            Faculty = sso.Speciality?.Department?.Institute?.InstituteName,
            Speciality = sso.Speciality?.SpecialityName,
            Course = sso.Course,
            GrantName = activeGrant?.Name,
            GrantAmount = activeGrant?.Amount ?? 0,
            ScholarshipName = activeScholarship?.Name,
            ScholarshipAmount = activeScholarship?.Amount,
            ScholarshipLostDate = latestScholarship?.LostDate,
            ScholarshipOrderLostDate = latestScholarship?.OrderLostDate,
            ScholarshipOrderCandidateDate = latestScholarship?.OrderCandidateDate,
            ScholarshipNotes = latestScholarship?.Notes,
            iban = sso.iban,
            isActive = sso.IsActive
        };
    }
}
