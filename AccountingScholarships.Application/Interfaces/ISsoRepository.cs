using System.Linq.Expressions;

namespace AccountingScholarships.Application.Interfaces;

public interface ISsoRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> GetAllWithIncludesAsync(string[] includes, CancellationToken cancellationToken = default);
    Task<T?> FindFirstWithIncludesAsync(Expression<Func<T, bool>> predicate, string[] includes, CancellationToken cancellationToken = default);
}
