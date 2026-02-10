using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Domain.Entities;
using AccountingScholarships.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class ScholarshipRepository : Repository<Scholarship>, IScholarshipRepository
{
    public ScholarshipRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Scholarship>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Scholarship>()
            .Where(s => s.StudentId == studentId)
            .OrderByDescending(s => s.StartDate)
            .ToListAsync(cancellationToken);
    }
}
