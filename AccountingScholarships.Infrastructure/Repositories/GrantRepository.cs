using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Domain.Entities.Testing.Grants;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class GrantRepository : Repository<Grant>, IGrantRepository
{
    public GrantRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Grant>> GetByStudentIdAsync(int studentId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Grant>()
            .Where(g => g.StudentId == studentId)
            .OrderByDescending(g => g.StartDate)
            .ToListAsync(cancellationToken);
    }
}
