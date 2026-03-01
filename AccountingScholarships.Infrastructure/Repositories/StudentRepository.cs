using AccountingScholarships.Domain.Entities;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Student?> GetByIINAsync(string iin, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(s => s.Grants)
            .Include(s => s.Scholarships)
            .Include(s => s.Speciality)
                .ThenInclude(sp => sp!.Department)
                    .ThenInclude(d => d.Institute)
            .Include(s => s.StudyForm)
            .Include(s => s.DegreeLevel)
            .Include(s => s.Bank)
            .AsSplitQuery()
            .FirstOrDefaultAsync(s => s.IIN == iin, cancellationToken);
    }

    public async Task<Student?> GetWithDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(s => s.Grants)
            .Include(s => s.Scholarships)
            .Include(s => s.Speciality)
                .ThenInclude(sp => sp!.Department)
                    .ThenInclude(d => d.Institute)
            .Include(s => s.StudyForm)
            .Include(s => s.DegreeLevel)
            .Include(s => s.Bank)
            .AsSplitQuery()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Student>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(s => s.Grants)
            .Include(s => s.Scholarships)
            .Include(s => s.Speciality)
                .ThenInclude(sp => sp!.Department)
                    .ThenInclude(d => d.Institute)
            .Include(s => s.StudyForm)
            .Include(s => s.DegreeLevel)
            .Include(s => s.Bank)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Dictionary<string, Student>> GetAllAsDictionaryByIINAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .ToDictionaryAsync(s => s.IIN, cancellationToken);
    }

    public async Task<IReadOnlyList<Student>> FindByIINsAsync(IList<string> iins, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(s => iins.Contains(s.IIN))
            .Include(s => s.Grants)
            .Include(s => s.Scholarships)
            .Include(s => s.Speciality)
                .ThenInclude(sp => sp!.Department)
                    .ThenInclude(d => d.Institute)
            .Include(s => s.StudyForm)
            .Include(s => s.DegreeLevel)
            .Include(s => s.Bank)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }
}
