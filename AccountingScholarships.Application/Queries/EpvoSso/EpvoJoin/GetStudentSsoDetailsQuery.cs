using AccountingScholarships.Domain.DTO.EpvoSso.EpvoJoin;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso.EpvoJoin;

public record GetStudentSsoDetailsQuery : IRequest<IReadOnlyList<StudentSsoDetailDto>>;
