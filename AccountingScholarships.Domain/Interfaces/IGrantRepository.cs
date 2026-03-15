using AccountingScholarships.Domain.Entities.Grants;

namespace AccountingScholarships.Domain.Interfaces;

public interface IGrantRepository : IRepository<Grant>
{
    Task<IReadOnlyList<Grant>> GetByStudentIdAsync(int studentId, CancellationToken cancellationToken = default);
}
