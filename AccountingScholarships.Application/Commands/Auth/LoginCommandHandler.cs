
using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Domain.Entities;
using MediatR;

namespace AccountingScholarships.Application.Commands.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResponseDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Users.FindAsync(
            u => u.Username == request.Login.Username, cancellationToken);

        var user = users.FirstOrDefault();

        if (user is null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(request.Login.Password, user.PasswordHash))
            return null;

        var token = _jwtTokenService.GenerateToken(user.Id.ToString(), user.Username, user.Role);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            Role = user.Role,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };
    }
}
