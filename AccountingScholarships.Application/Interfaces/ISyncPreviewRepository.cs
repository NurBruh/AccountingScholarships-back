using AccountingScholarships.Application.DTO;

namespace AccountingScholarships.Application.Interfaces;

public interface ISyncPreviewRepository
{
    /// <summary>
    /// Возвращает предпросмотр синхронизации с пагинацией.
    /// Базируется на данных ComparisonRepository (ССО vs ЕПВО).
    /// Показывает только записи с различиями и отсутствующие в ЕПВО.
    /// </summary>
    Task<SyncPreviewComparisonPagedDto> GetPreviewAsync(
        int page,
        int pageSize,
        string filter,
        string? search,
        CancellationToken ct = default);

    /// <summary>
    /// Сохраняет записи по ИИН в STUDENT_TEMP.
    /// Трансформация ССО → ЕПВО выполняется внутри через SsoToEpvoMapperService.
    /// Не удаляет существующие записи (Upsert по StudentId).
    /// </summary>
    Task<int> SaveToTempAsync(
        List<string> iins,
        CancellationToken ct = default);
}
