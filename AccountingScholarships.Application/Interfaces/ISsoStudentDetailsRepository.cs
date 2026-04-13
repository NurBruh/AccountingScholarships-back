using AccountingScholarships.Application.DTO.EpvoSso.EpvoJoin;

namespace AccountingScholarships.Application.Interfaces;

public interface ISsoStudentDetailsRepository
{
    Task<IReadOnlyList<StudentSsoDetailDto>> GetAllAsync(CancellationToken ct = default);
}
