using AccountingScholarships.Domain.Entities.Epvo;

namespace AccountingScholarships.Domain.Interfaces;

public interface IEpvoRepository
{
    Task<IReadOnlyList<EpvoStudent>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Dictionary<string, EpvoStudent>> GetAllAsDictionaryByIINAsync(CancellationToken cancellationToken = default);
    Task<EpvoStudent?> GetByIINAsync(string iin, CancellationToken cancellationToken = default);
    Task<EpvoStudent?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<EpvoStudent> AddAsync(EpvoStudent entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(EpvoStudent entity, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
