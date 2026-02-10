using AccountingScholarships.Application.DTOs;
using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Domain.Entities;
using MediatR;

namespace AccountingScholarships.Application.Features.Auth.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IRepository<User> _userRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public RegisterCommandHandler(IRepository<User> userRepository, IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existing = await _userRepository.FindAsync(
            u => u.Username == request.Register.Username, cancellationToken);

        if (existing.Any())
            throw new InvalidOperationException("ѕользователь с таким именем уже существует.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Register.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Register.Password),
            Email = request.Register.Email,
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user, cancellationToken);

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
