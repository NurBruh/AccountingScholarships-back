using AccountingScholarships.Domain.Entities;

namespace AccountingScholarships.Domain.Interfaces;

public interface IScholarshipRepository : IRepository<Scholarship>
{
    Task<IReadOnlyList<Scholarship>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default);
}
