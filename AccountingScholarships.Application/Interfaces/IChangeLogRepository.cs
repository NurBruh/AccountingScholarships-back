using AccountingScholarships.Domain.Entities.Real.epvosso;

namespace AccountingScholarships.Application.Interfaces;

/// <summary>
/// Репозиторий для записи и чтения истории изменений полей студентов.
/// </summary>
public interface IChangeLogRepository
{
    /// <summary>
    /// Записывает пакет изменений в STUDENT_CHANGE_LOG.
    /// </summary>
    Task SaveChangesAsync(IEnumerable<StudentChangeLog> changes, CancellationToken ct = default);

    /// <summary>
    /// Возвращает постраничный список изменений (все студенты).
    /// </summary>
    Task<ChangeLogPagedResult> GetChangeLogsAsync(string? iin, int page, int pageSize, CancellationToken ct = default);

    /// <summary>
    /// Возвращает изменения по конкретному студенту (ИИН).
    /// </summary>
    Task<IReadOnlyList<StudentChangeLog>> GetChangeLogsByIinAsync(string iin, CancellationToken ct = default);
}

public class ChangeLogPagedResult
{
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public IReadOnlyList<StudentChangeLog> Items { get; set; } = Array.Empty<StudentChangeLog>();
}
