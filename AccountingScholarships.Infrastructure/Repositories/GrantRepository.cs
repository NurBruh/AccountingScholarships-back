using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Domain.Entities;
using AccountingScholarships.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class GrantRepository : Repository<Grant>, IGrantRepository
{
    public GrantRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Grant>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Grant>()
            .Where(g => g.StudentId == studentId)
            .OrderByDescending(g => g.StartDate)
            .ToListAsync(cancellationToken);
    }
}
