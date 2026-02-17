using AccountingScholarships.Domain.Entities;

namespace AccountingScholarships.Domain.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Task<Student?> GetByIINAsync(string iin, CancellationToken cancellationToken = default);
    Task<Student?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
}
