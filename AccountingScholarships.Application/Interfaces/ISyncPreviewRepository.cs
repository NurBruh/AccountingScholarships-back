using AccountingScholarships.Application.DTO;
using AccountingScholarships.Application.DTO.EpvoSso;

namespace AccountingScholarships.Application.Interfaces;

public interface ISyncPreviewRepository
{
    /// <summary>
    /// Возвращает предпросмотр синхронизации с пагинацией.
    /// Показывает только записи с различиями и отсутствующие в ЕПВО.
    /// </summary>
    Task<SyncPreviewComparisonPagedDto> GetPreviewAsync(
        int page,
        int pageSize,
        string filter,
        string? search,
        CancellationToken ct = default);

    /// <summary>
    /// Сохраняет выбранные записи в STUDENT_TEMP с указанным SessionId.
    /// Не удаляет существующие записи.
    /// </summary>
    Task<int> SaveToTempAsync(
        List<EpvoStudentTempDto> items,
        string sessionId,
        CancellationToken ct = default);
}
