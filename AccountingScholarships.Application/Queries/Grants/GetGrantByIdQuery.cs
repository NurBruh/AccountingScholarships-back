
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.Grants;

public record GetGrantByIdQuery(int Id) : IRequest<GrantDto?>;
