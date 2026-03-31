using AccountingScholarships.Domain.DTO.EpvoSso;

namespace AccountingScholarships.Domain.Interfaces;

public interface ISsoStudentDetailsRepository
{
    Task<IReadOnlyList<StudentSsoDetailDto>> GetAllAsync(CancellationToken ct = default);
}
