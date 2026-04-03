using AccountingScholarships.Domain.Common;
using AccountingScholarships.Domain.Entities.Real.epvosso;

namespace AccountingScholarships.Domain.Interfaces;

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
    Task<SendTempResult> SendTempToEpvoAsync(CancellationToken ct = default);
}
