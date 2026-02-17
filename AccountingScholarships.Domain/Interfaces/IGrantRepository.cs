using AccountingScholarships.Domain.Entities;

namespace AccountingScholarships.Domain.Interfaces;

public interface IGrantRepository : IRepository<Grant>
{
    Task<IReadOnlyList<Grant>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default);
}
