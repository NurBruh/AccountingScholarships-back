using AccountingScholarships.Application.Common;

namespace AccountingScholarships.Application.Interfaces;

public interface IPreviewRepository
{
    Task<SyncPreviewResult> GetSyncPreviewAsync(CancellationToken ct = default);
}
