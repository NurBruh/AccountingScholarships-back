
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Commands.Auth;

public record RegisterCommand(RegisterDto Register) : IRequest<AuthResponseDto>;
