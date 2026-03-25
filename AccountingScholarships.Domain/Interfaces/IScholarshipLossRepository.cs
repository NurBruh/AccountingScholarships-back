using AccountingScholarships.Domain.Entities.Testing.Scholarships;

namespace AccountingScholarships.Domain.Interfaces;

public interface IScholarshipLossRepository
{
    Task<IReadOnlyList<ScholarshipLossRecord>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ScholarshipLossRecord?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ScholarshipLossRecord> AddAsync(ScholarshipLossRecord entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
