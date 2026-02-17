
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.Grants;

public record GetGrantsByStudentIdQuery(int StudentId) : IRequest<IReadOnlyList<GrantDto>>;
