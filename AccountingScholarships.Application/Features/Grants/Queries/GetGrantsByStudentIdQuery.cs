using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Grants.Queries;

public record GetGrantsByStudentIdQuery(Guid StudentId) : IRequest<IReadOnlyList<GrantDto>>;
