using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetEduPositionCategoryByIdQuery(int Id) : IRequest<Edu_PositionCategoriesDto?>;
