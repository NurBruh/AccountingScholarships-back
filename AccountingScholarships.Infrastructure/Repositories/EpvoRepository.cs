using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Domain.Entities.Testing.Epvo;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class EpvoRepository : IEpvoRepository
{
    private readonly EpvoDbContext _context;

    public EpvoRepository(EpvoDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<EpvoStudent>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.EpvoStudents
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Dictionary<string, EpvoStudent>> GetAllAsDictionaryByIINAsync(CancellationToken cancellationToken = default)
    {
        return await _context.EpvoStudents
            .ToDictionaryAsync(s => s.IIN, cancellationToken);
    }

    public async Task<EpvoStudent?> GetByIINAsync(string iin, CancellationToken cancellationToken = default)
    {
        return await _context.EpvoStudents.FirstOrDefaultAsync(s => s.IIN == iin, cancellationToken);
    }

    public async Task<EpvoStudent?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.EpvoStudents.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<EpvoStudent> AddAsync(EpvoStudent entity, CancellationToken cancellationToken = default)
    {
        await _context.EpvoStudents.AddAsync(entity, cancellationToken);
        return entity;
    }

    public Task UpdateAsync(EpvoStudent entity, CancellationToken cancellationToken = default)
    {
        _context.EpvoStudents.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
