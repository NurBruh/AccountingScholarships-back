using AccountingScholarships.Domain;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class PosrednikRepository : IPosrednikRepository
{
    private readonly ApplicationDbContext _context;

    public PosrednikRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<EpvoPosrednik>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.EpvoPosredniki
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Dictionary<string, EpvoPosrednik>> GetAllAsDictionaryByIINAsync(CancellationToken cancellationToken = default)
    {
        return await _context.EpvoPosredniki
            .ToDictionaryAsync(s => s.IIN, cancellationToken);
    }

    public async Task<IReadOnlyList<EpvoPosrednik>> FindByIINsAsync(IList<string> iins, CancellationToken cancellationToken = default)
    {
        return await _context.EpvoPosredniki
            .Where(s => iins.Contains(s.IIN))
            .ToListAsync(cancellationToken);
    }

    public async Task<EpvoPosrednik?> GetByIINAsync(string iin, CancellationToken cancellationToken = default)
    {
        return await _context.EpvoPosredniki.FirstOrDefaultAsync(s => s.IIN == iin, cancellationToken);
    }

    public async Task<EpvoPosrednik> AddAsync(EpvoPosrednik entity, CancellationToken cancellationToken = default)
    {
        await _context.EpvoPosredniki.AddAsync(entity, cancellationToken);
        return entity;
    }

    public Task UpdateAsync(EpvoPosrednik entity, CancellationToken cancellationToken = default)
    {
        _context.EpvoPosredniki.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
