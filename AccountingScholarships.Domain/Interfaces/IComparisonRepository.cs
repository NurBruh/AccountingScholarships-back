using AccountingScholarships.Domain.DTO;

namespace AccountingScholarships.Domain.Interfaces;

public interface IComparisonRepository
{
    Task<IReadOnlyList<StudentComparisonDto>> GetComparisonAsync(CancellationToken ct = default);
}
