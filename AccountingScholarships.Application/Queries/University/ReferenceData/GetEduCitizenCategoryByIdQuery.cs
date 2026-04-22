using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduCitizenCategoryByIdQuery(int Id) : IRequest<Edu_CitizenCategoriesDto?>;
