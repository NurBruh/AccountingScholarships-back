using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduPositionCategoriesQuery : IRequest<IReadOnlyList<Edu_PositionCategoriesDto>>;
