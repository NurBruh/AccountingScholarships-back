using AccountingScholarships.Application.Common;
using AccountingScholarships.Domain.Entities.Real.epvosso;

namespace AccountingScholarships.Application.Interfaces;

/// <summary>
/// Репозиторий для вызова хранимых процедур в EPVO_test.
/// </summary>
public interface IStoredProcedureRepository
{
    /// <summary>
    /// Выполняет [dbo].[Reload_STUDENT] и возвращает результат.
    /// </summary>
    Task<StoredProcedureResult> ExecuteReloadStudentAsync(CancellationToken ct = default);

    /// <summary>
    /// Выполняет [dbo].[Reload_STUDENT] и возвращает SELECT-результат (как в SSMS).
    /// </summary>
    Task<List<Dictionary<string, object?>>> ReadReloadStudentAsync(CancellationToken ct = default);

    /// <summary>
    /// Выполняет [dbo].[Reload_STUDENT] и сохраняет результат в STUDENT_TEMP.
    /// Перед вставкой очищает STUDENT_TEMP. Возвращает количество вставленных записей.
    /// </summary>
    Task<int> SaveReloadStudentToTempAsync(CancellationToken ct = default);

    /// <summary>
    /// Читает записи из STUDENT_TEMP, симулирует отправку в ЕПВО,
    /// записывает результат каждой попытки в STUDENT_SYNC_LOG.
    /// STUDENT_SSO не затрагивается.
    /// </summary>
    Task<SendTempResult> SendTempToEpvoAsync(string triggeredBy, CancellationToken ct = default);

    /// <summary>
    /// Выполняет [dbo].[Reload_STUDENT] (read-only), проверяет дубликаты по iinPlt
    /// против STUDENT_SSO и STUDENT. Возвращает список с флагом IsDuplicate и статистику.
    /// </summary>
    Task<SyncPreviewResult> GetSyncPreviewAsync(CancellationToken ct = default);

    /// <summary>
    /// Возвращает постраничный список логов синхронизации с ЕПВО.
    /// </summary>
    Task<SyncLogPagedResult> GetSyncLogsAsync(string? status, int page, int pageSize, CancellationToken ct = default);

    /// <summary>
    /// Возвращает все логи по конкретному студенту.
    /// </summary>
    Task<IReadOnlyList<StudentSyncLog>> GetSyncLogsByStudentAsync(int studentId, CancellationToken ct = default);
}

