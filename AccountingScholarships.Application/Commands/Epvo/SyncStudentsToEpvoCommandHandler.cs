using AccountingScholarships.Domain.Entities.Epvo;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

public class SyncStudentsToEpvoCommandHandler : IRequestHandler<SyncStudentsToEpvoCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEpvoRepository _epvoRepository;

    public SyncStudentsToEpvoCommandHandler(IUnitOfWork unitOfWork, IEpvoRepository epvoRepository)
    {
        _unitOfWork = unitOfWork;
        _epvoRepository = epvoRepository;
    }

    public async Task<int> Handle(SyncStudentsToEpvoCommand request, CancellationToken cancellationToken)
    {
        // Получаем всех студентов из SSO с грантами и стипендиями
        var ssoStudents = await _unitOfWork.Students.GetAllWithDetailsAsync(cancellationToken);

        // Предзагрузка ВСЕХ записей ЕПВО одним запросом (вместо N+1)
        var epvoMap = await _epvoRepository.GetAllAsDictionaryByIINAsync(cancellationToken);
        var syncedCount = 0;

        foreach (var sso in ssoStudents)
        {
            // Берём первый активный грант и стипендию (если есть)
            var activeGrant = sso.Grants?.FirstOrDefault(g => g.IsActive);
            var activeScholarship = sso.Scholarships?.FirstOrDefault(s => s.IsActive);
            // Notes и даты лишения берём из последней стипендии (даже неактивной)
            var latestScholarship = sso.Scholarships?.OrderByDescending(s => s.CreatedAt).FirstOrDefault();

            if (!epvoMap.TryGetValue(sso.IIN, out var existing))
            {
                // Создаём новую запись в ЕПВО
                var epvoStudent = new EpvoStudent
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

                await _epvoRepository.AddAsync(epvoStudent, cancellationToken);
                syncedCount++;
            }
            else
            {
                // Обновляем существующую запись в ЕПВО
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
                existing.IsActive = sso.IsActive;
                existing.iban = sso.iban;
                existing.SyncDate = DateTime.UtcNow;

                await _epvoRepository.UpdateAsync(existing, cancellationToken);
                syncedCount++;
            }
        }

        // Один SaveChanges в конце — вместо отдельного на каждую запись
        await _epvoRepository.SaveChangesAsync(cancellationToken);

        return syncedCount;
    }
}
