using AccountingScholarships.Domain.Entities;

namespace AccountingScholarships.Application.Interfaces;

public interface IGrantRepository : IRepository<Grant>
{
    Task<IReadOnlyList<Grant>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default);
}
