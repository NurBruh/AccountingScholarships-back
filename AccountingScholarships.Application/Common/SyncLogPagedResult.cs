using AccountingScholarships.Domain.Entities.Real.epvosso;

namespace AccountingScholarships.Application.Common;

public class SyncLogPagedResult
{
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public IReadOnlyList<StudentSyncLog> Logs { get; set; } = Array.Empty<StudentSyncLog>();
}
