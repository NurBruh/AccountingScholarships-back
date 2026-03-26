using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public record GetAllEduSpecialitiesQuery : IRequest<IReadOnlyList<Edu_SpecialitiesDto>>;
