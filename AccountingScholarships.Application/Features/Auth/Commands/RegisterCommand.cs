using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Auth.Commands;

public record RegisterCommand(RegisterDto Register) : IRequest<AuthResponseDto>;
