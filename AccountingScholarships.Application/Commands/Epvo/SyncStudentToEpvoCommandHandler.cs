using AccountingScholarships.Domain.Entities.Epvo;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

public class SyncStudentToEpvoCommandHandler : IRequestHandler<SyncStudentToEpvoCommand, bool>
{
    private readonly IPosrednikRepository _posrednikRepository;
    private readonly IEpvoRepository _epvoRepository;

    public SyncStudentToEpvoCommandHandler(IPosrednikRepository posrednikRepository, IEpvoRepository epvoRepository)
    {
        _posrednikRepository = posrednikRepository;
        _epvoRepository = epvoRepository;
    }

    public async Task<bool> Handle(SyncStudentToEpvoCommand request, CancellationToken cancellationToken)
    {
        var posrednik = await _posrednikRepository.GetByIINAsync(request.IIN, cancellationToken);
        if (posrednik is null) return false;

        var existing = await _epvoRepository.GetByIINAsync(request.IIN, cancellationToken);

        if (existing is null)
        {
            var epvoStudent = new EpvoStudent
            {
                FirstName = posrednik.FirstName,
                LastName = posrednik.LastName,
                MiddleName = posrednik.MiddleName,
                IIN = posrednik.IIN,
                DateOfBirth = posrednik.DateOfBirth,
                Faculty = posrednik.Faculty,
                Speciality = posrednik.Speciality,
                Course = posrednik.Course,
                GrantName = posrednik.GrantName,
                GrantAmount = posrednik.GrantAmount,
                ScholarshipName = posrednik.ScholarshipName,
                ScholarshipAmount = posrednik.ScholarshipAmount,
                iban = posrednik.iban,
                IsActive = posrednik.IsActive,
                SyncDate = DateTime.UtcNow
            };

            await _epvoRepository.AddAsync(epvoStudent, cancellationToken);
        }
        else
        {
            existing.FirstName = posrednik.FirstName;
            existing.LastName = posrednik.LastName;
            existing.MiddleName = posrednik.MiddleName;
            existing.DateOfBirth = posrednik.DateOfBirth;
            existing.Faculty = posrednik.Faculty;
            existing.Speciality = posrednik.Speciality;
            existing.Course = posrednik.Course;
            existing.GrantName = posrednik.GrantName;
            existing.GrantAmount = posrednik.GrantAmount;
            existing.ScholarshipName = posrednik.ScholarshipName;
            existing.ScholarshipAmount = posrednik.ScholarshipAmount;
            existing.iban = posrednik.iban;
            existing.IsActive = posrednik.IsActive;
            existing.SyncDate = DateTime.UtcNow;

            await _epvoRepository.UpdateAsync(existing, cancellationToken);
        }

        // Сохраняем изменения одним вызовом
        await _epvoRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}
