using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Grants.Commands;

public record UpdateGrantCommand(Guid Id, UpdateGrantDto Dto) : IRequest<GrantDto?>;
