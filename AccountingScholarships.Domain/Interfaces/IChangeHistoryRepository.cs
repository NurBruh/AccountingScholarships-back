using AccountingScholarships.Domain.Entities.Reference;

namespace AccountingScholarships.Domain.Interfaces;

public interface IChangeHistoryRepository
{
    Task<IReadOnlyList<ChangeHistoryRecord>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ChangeHistoryRecord>> GetByIINAsync(string iin, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<ChangeHistoryRecord> records, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
