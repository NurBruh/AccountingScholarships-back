using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Grants.Queries;

public record GetGrantByIdQuery(Guid Id) : IRequest<GrantDto?>;
