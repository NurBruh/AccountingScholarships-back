using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Auth.Commands;

public record LoginCommand(LoginDto Login) : IRequest<AuthResponseDto?>;
