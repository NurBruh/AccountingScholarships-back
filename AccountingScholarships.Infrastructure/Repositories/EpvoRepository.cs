using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Domain.Entities.Epvo;
using AccountingScholarships.Infrastructure.Persistence;
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
        return await _context.EpvoStudents.ToListAsync(cancellationToken);
    }

    public async Task<EpvoStudent?> GetByIINAsync(string iin, CancellationToken cancellationToken = default)
    {
        return await _context.EpvoStudents.FirstOrDefaultAsync(s => s.IIN == iin, cancellationToken);
    }

    public async Task<EpvoStudent?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.EpvoStudents.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<EpvoStudent> AddAsync(EpvoStudent entity, CancellationToken cancellationToken = default)
    {
        await _context.EpvoStudents.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(EpvoStudent entity, CancellationToken cancellationToken = default)
    {
        _context.EpvoStudents.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
