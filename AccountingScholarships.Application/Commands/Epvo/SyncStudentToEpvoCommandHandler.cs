using AccountingScholarships.Domain.Entities.Epvo;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

public class SyncStudentToEpvoCommandHandler : IRequestHandler<SyncStudentToEpvoCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEpvoRepository _epvoRepository;

    public SyncStudentToEpvoCommandHandler(IUnitOfWork unitOfWork, IEpvoRepository epvoRepository)
    {
        _unitOfWork = unitOfWork;
        _epvoRepository = epvoRepository;
    }

    public async Task<bool> Handle(SyncStudentToEpvoCommand request, CancellationToken cancellationToken)
    {
        var sso = await _unitOfWork.Students.GetByIINAsync(request.IIN, cancellationToken);
        if (sso is null) return false;

        var activeGrant = sso.Grants?.FirstOrDefault(g => g.IsActive);
        var activeScholarship = sso.Scholarships?.FirstOrDefault(s => s.IsActive);
        var latestScholarship = sso.Scholarships?.OrderByDescending(s => s.CreatedAt).FirstOrDefault();

        var faculty = sso.Speciality?.Department?.Institute?.InstituteName;
        var speciality = sso.Speciality?.SpecialityName;

        var existing = await _epvoRepository.GetByIINAsync(request.IIN, cancellationToken);

        if (existing is null)
        {
            var epvoStudent = new EpvoStudent
            {
                FirstName = sso.FirstName,
                LastName = sso.LastName,
                MiddleName = sso.MiddleName,
                IIN = sso.IIN,
                DateOfBirth = sso.DateOfBirth,
                Faculty = faculty,
                Speciality = speciality,
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

            await _epvoRepository.AddAsync(epvoStudent, cancellationToken);
        }
        else
        {
            existing.FirstName = sso.FirstName;
            existing.LastName = sso.LastName;
            existing.MiddleName = sso.MiddleName;
            existing.DateOfBirth = sso.DateOfBirth;
            existing.Faculty = faculty;
            existing.Speciality = speciality;
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

            await _epvoRepository.UpdateAsync(existing, cancellationToken);
        }

        await _epvoRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}
