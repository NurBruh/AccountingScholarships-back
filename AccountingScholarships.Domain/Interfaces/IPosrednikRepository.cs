using AccountingScholarships.Domain;

namespace AccountingScholarships.Domain.Interfaces;

public interface IPosrednikRepository
{
    Task<IReadOnlyList<EpvoPosrednik>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Dictionary<string, EpvoPosrednik>> GetAllAsDictionaryByIINAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EpvoPosrednik>> FindByIINsAsync(IList<string> iins, CancellationToken cancellationToken = default);
    Task<EpvoPosrednik?> GetByIINAsync(string iin, CancellationToken cancellationToken = default);
    Task<EpvoPosrednik> AddAsync(EpvoPosrednik entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(EpvoPosrednik entity, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
