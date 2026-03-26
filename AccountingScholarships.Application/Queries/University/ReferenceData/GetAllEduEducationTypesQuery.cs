using AccountingScholarships.Domain.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetAllEduEducationTypesQuery : IRequest<IReadOnlyList<Edu_EducationTypesDto>>;
