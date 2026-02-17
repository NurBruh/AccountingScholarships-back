
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.Grants;

public record GetGrantByIdQuery(Guid Id) : IRequest<GrantDto?>;
