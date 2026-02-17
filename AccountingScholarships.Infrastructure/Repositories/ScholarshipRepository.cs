using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Domain.Entities;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class ScholarshipRepository : Repository<Scholarship>, IScholarshipRepository
{
    public ScholarshipRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Scholarship>> GetByStudentIdAsync(int studentId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Scholarship>()
            .Where(s => s.StudentId == studentId)
            .OrderByDescending(s => s.StartDate)
            .ToListAsync(cancellationToken);
    }
}
