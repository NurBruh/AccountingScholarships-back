
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Commands.Auth;

public record LoginCommand(LoginDto Login) : IRequest<AuthResponseDto?>;
