using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

/// <summary>
/// Обработчик: собирает изменённых студентов (Посредник vs ЕПВО),
/// формирует массив и одним POST отправляет в ЕПВО через IEpvoApiClient.
/// </summary>
public class SyncChangedStudentsToEpvoCommandHandler : IRequestHandler<SyncChangedStudentsToEpvoCommand, int>
{
    private readonly IPosrednikRepository _posrednikRepository;
    private readonly IEpvoRepository _epvoRepository;
    private readonly IEpvoApiClient _epvoApiClient;

    public SyncChangedStudentsToEpvoCommandHandler(
        IPosrednikRepository posrednikRepository,
        IEpvoRepository epvoRepository,
        IEpvoApiClient epvoApiClient)
    {
        _posrednikRepository = posrednikRepository;
        _epvoRepository = epvoRepository;
        _epvoApiClient = epvoApiClient;
    }

    public async Task<int> Handle(SyncChangedStudentsToEpvoCommand request, CancellationToken cancellationToken)
    {
        // 1. Получаем все записи из посредника (актуальные данные SSO)
        var posrednikStudents = await _posrednikRepository.GetAllAsync(cancellationToken);

        // 2. Предзагрузка ВСЕХ записей ЕПВО одним запросом (вместо N+1)
        var epvoMap = await _epvoRepository.GetAllAsDictionaryByIINAsync(cancellationToken);

        // 3. Формируем массив изменённых записей
        var changedPayload = new List<EpvoSendPayloadDto>();

        foreach (var pos in posrednikStudents)
        {
            // Если нет в ЕПВО — значит новый, добавляем
            if (!epvoMap.TryGetValue(pos.IIN, out var epvo))
            {
                changedPayload.Add(MapToPayload(pos));
                continue;
            }

            // Сравниваем поле за полем
            if (HasDifferences(pos, epvo))
            {
                changedPayload.Add(MapToPayload(pos));
            }
        }

        if (changedPayload.Count == 0)
            return 0;

        // 4. Один POST-запрос с массивом всех изменённых студентов
        await _epvoApiClient.SendStudentsAsync(changedPayload, cancellationToken);

        return changedPayload.Count;
    }

    /// <summary>
    /// Сравниваем данные Посредника с записью в ЕПВО — есть ли расхождения.
    /// </summary>
    private static bool HasDifferences(Domain.EpvoPosrednik pos, Domain.Entities.Epvo.EpvoStudent epvo)
    {
        return pos.FirstName != epvo.FirstName
            || pos.LastName != epvo.LastName
            || pos.MiddleName != epvo.MiddleName
            || pos.Faculty != epvo.Faculty
            || pos.Speciality != epvo.Speciality
            || pos.Course != epvo.Course
            || pos.GrantName != epvo.GrantName
            || pos.GrantAmount != epvo.GrantAmount
            || pos.ScholarshipName != epvo.ScholarshipName
            || pos.ScholarshipAmount != epvo.ScholarshipAmount
            || pos.iban != epvo.iban
            || pos.IsActive != epvo.IsActive;
    }

    /// <summary>
    /// Маппинг записи посредника в DTO для отправки в ЕПВО.
    /// </summary>
    private static EpvoSendPayloadDto MapToPayload(Domain.EpvoPosrednik pos)
    {
        return new EpvoSendPayloadDto
        {
            IIN = pos.IIN,
            FirstName = pos.FirstName,
            LastName = pos.LastName,
            MiddleName = pos.MiddleName,
            Faculty = pos.Faculty,
            Speciality = pos.Speciality,
            Course = pos.Course,
            GrantName = pos.GrantName,
            GrantAmount = pos.GrantAmount ?? 0,
            ScholarshipName = pos.ScholarshipName,
            ScholarshipAmount = pos.ScholarshipAmount,
            iban = pos.iban,
            isActive = pos.IsActive
        };
    }
}
