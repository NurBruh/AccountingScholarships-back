using AccountingScholarships.Application.Common;
using AccountingScholarships.Application.DTO;
using AccountingScholarships.Application.DTO.EpvoSso.EpvoJoin;

namespace AccountingScholarships.Application.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Аутентификация пользователя и определение его роли.
    /// </summary>
    Task<AuthResult<SsoAuthResponseDto>> LoginAsync(SsoLoginDto dto, CancellationToken ct = default);

    /// <summary>
    /// Получить список студентов эдвайзера по его ID.
    /// </summary>
    Task<AuthResult<IReadOnlyList<StudentSsoDetailDto>>> GetAdvisorStudentsAsync(int advisorId, CancellationToken ct = default);

    /// <summary>
    /// Получить список студентов директора института по его ID.
    /// </summary>
    Task<AuthResult<IReadOnlyList<StudentSsoDetailDto>>> GetDirectorStudentsAsync(int userId, CancellationToken ct = default);
}
