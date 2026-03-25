using AccountingScholarships.Domain.Entities.Testing.Scholarships;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class ScholarshipLossRepository : IScholarshipLossRepository
{
    private readonly ApplicationDbContext _context;

    public ScholarshipLossRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ScholarshipLossRecord>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ScholarshipLossRecords
            .OrderByDescending(r => r.LostDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<ScholarshipLossRecord?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.ScholarshipLossRecords
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<ScholarshipLossRecord> AddAsync(ScholarshipLossRecord entity, CancellationToken cancellationToken = default)
    {
        await _context.ScholarshipLossRecords.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            _context.ScholarshipLossRecords.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
