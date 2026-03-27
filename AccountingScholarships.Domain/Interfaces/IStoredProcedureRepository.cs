using AccountingScholarships.Domain.Common;

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
}
