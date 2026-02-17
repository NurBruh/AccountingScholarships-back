
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.Grants;

public record GetGrantsByStudentIdQuery(Guid StudentId) : IRequest<IReadOnlyList<GrantDto>>;
