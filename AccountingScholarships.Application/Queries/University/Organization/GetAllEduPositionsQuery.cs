using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public record GetAllEduPositionsQuery : IRequest<IReadOnlyList<Edu_PositionsDto>>;
