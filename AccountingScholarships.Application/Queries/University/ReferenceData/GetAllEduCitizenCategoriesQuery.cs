using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetAllEduCitizenCategoriesQuery : IRequest<IReadOnlyList<Edu_CitizenCategoriesDto>>;
