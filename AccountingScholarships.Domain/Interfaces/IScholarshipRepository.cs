using AccountingScholarships.Domain.Entities.Scholarships;

namespace AccountingScholarships.Domain.Interfaces;

public interface IScholarshipRepository : IRepository<Scholarship>
{
    Task<IReadOnlyList<Scholarship>> GetByStudentIdAsync(int studentId, CancellationToken cancellationToken = default);
}
