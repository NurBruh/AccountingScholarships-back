using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Entities.Epvo;
using AccountingScholarships.Domain.Interfaces;

namespace AccountingScholarships.Infrastructure.Services;

/// <summary>
/// Тестовая реализация: вместо реального ЕПВО API пишем напрямую в нашу EpvoStudents таблицу.
/// Когда получим реальный ЕПВО API — заменить реализацию на HttpClient.
/// </summary>
public class EpvoApiClient : IEpvoApiClient
{
    private readonly IEpvoRepository _epvoRepository;

    public EpvoApiClient(IEpvoRepository epvoRepository)
    {
        _epvoRepository = epvoRepository;
    }

    public async Task SendStudentsAsync(IList<EpvoSendPayloadDto> students, CancellationToken cancellationToken = default)
    {
        foreach (var dto in students)
        {
            var existing = await _epvoRepository.GetByIINAsync(dto.IIN, cancellationToken);

            if (existing is null)
            {
                var newStudent = new EpvoStudent
                {
                    IIN              = dto.IIN,
                    FirstName        = dto.FirstName,
                    LastName         = dto.LastName,
                    MiddleName       = dto.MiddleName,
                    Faculty          = dto.Faculty,
                    Speciality       = dto.Speciality,
                    Course           = dto.Course,
                    GrantName        = dto.GrantName,
                    GrantAmount      = dto.GrantAmount,
                    ScholarshipName  = dto.ScholarshipName,
                    ScholarshipAmount = dto.ScholarshipAmount,
                    iban             = dto.iban,
                    IsActive         = dto.isActive,
                    SyncDate         = DateTime.UtcNow
                };
                await _epvoRepository.AddAsync(newStudent, cancellationToken);
            }
            else
            {
                existing.FirstName        = dto.FirstName;
                existing.LastName         = dto.LastName;
                existing.MiddleName       = dto.MiddleName;
                existing.Faculty          = dto.Faculty;
                existing.Speciality       = dto.Speciality;
                existing.Course           = dto.Course;
                existing.GrantName        = dto.GrantName;
                existing.GrantAmount      = dto.GrantAmount;
                existing.ScholarshipName  = dto.ScholarshipName;
                existing.ScholarshipAmount = dto.ScholarshipAmount;
                existing.iban             = dto.iban;
                existing.IsActive         = dto.isActive;
                existing.SyncDate         = DateTime.UtcNow;
                await _epvoRepository.UpdateAsync(existing, cancellationToken);
            }
        }
    }
}

