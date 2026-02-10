using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Domain.Entities;
using AccountingScholarships.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Student?> GetByIINAsync(string iin, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(s => s.IIN == iin, cancellationToken);
    }

    public async Task<Student?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(s => s.Grants)
            .Include(s => s.Scholarships)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
}
