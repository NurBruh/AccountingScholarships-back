using AccountingScholarships.Domain.Entities.Testing.Students;

namespace AccountingScholarships.Domain.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Task<Student?> GetByIINAsync(string iin, CancellationToken cancellationToken = default);
    Task<Student?> GetWithDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Student>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
    Task<Dictionary<string, Student>> GetAllAsDictionaryByIINAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Student>> FindByIINsAsync(IList<string> iins, CancellationToken cancellationToken = default);
}
