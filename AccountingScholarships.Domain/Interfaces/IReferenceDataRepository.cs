using AccountingScholarships.Domain.DTO;

namespace AccountingScholarships.Domain.Interfaces;

public interface IReferenceDataRepository
{
    Task<ReferenceDataDto> GetAllReferenceDataAsync(CancellationToken cancellationToken = default);
}
