using AccountingScholarships.Domain.Entities.Reference;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class ChangeHistoryRepository : IChangeHistoryRepository
{
    private readonly ApplicationDbContext _context;

    public ChangeHistoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ChangeHistoryRecord>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ChangeHistoryRecords
            .AsNoTracking()
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ChangeHistoryRecord>> GetByIINAsync(string iin, CancellationToken cancellationToken = default)
    {
        return await _context.ChangeHistoryRecords
            .AsNoTracking()
            .Where(r => r.StudentIIN == iin)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<ChangeHistoryRecord> records, CancellationToken cancellationToken = default)
    {
        await _context.ChangeHistoryRecords.AddRangeAsync(records, cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
