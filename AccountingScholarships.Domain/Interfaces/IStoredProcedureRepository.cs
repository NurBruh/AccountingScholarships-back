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
}
