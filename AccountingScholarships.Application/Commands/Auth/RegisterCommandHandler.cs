using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Domain.Entities.Testing.Users;
using MediatR;

namespace AccountingScholarships.Application.Commands.Auth;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.Users.FindAsync(
            u => u.Username == request.Register.Username, cancellationToken);

        if (existing.Any())
            throw new InvalidOperationException("Пользователь с таким именем уже существует.");

        var user = new User
        {
            Username = request.Register.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Register.Password),
            Email = request.Register.Email,
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Users.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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