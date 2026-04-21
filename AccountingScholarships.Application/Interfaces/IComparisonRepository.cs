using AccountingScholarships.Application.DTO;

namespace AccountingScholarships.Application.Interfaces;

public interface IComparisonRepository
{
    Task<IReadOnlyList<StudentComparisonDto>> GetComparisonAsync(CancellationToken ct = default);

    Task<object> SyncBatchAsync(List<string> iins, string triggeredBy, CancellationToken ct = default);
}
