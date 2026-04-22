using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduEducationTypeByIdQuery(int Id) : IRequest<Edu_EducationTypesDto?>;
