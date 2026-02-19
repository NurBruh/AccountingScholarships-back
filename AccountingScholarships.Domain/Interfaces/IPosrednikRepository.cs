using AccountingScholarships.Domain;

namespace AccountingScholarships.Domain.Interfaces;

public interface IPosrednikRepository
{
    Task<IReadOnlyList<EpvoPosrednik>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EpvoPosrednik?> GetByIINAsync(string iin, CancellationToken cancellationToken = default);
    Task<EpvoPosrednik> AddAsync(EpvoPosrednik entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(EpvoPosrednik entity, CancellationToken cancellationToken = default);
}
