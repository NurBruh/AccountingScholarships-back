using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public record GetAllEduPositionsQuery : IRequest<IReadOnlyList<Edu_PositionsDto>>;
