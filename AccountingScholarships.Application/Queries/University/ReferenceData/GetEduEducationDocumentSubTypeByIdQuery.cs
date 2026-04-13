using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduEducationDocumentSubTypeByIdQuery(int Id) : IRequest<Edu_EducationDocumentSubTypesDto?>;
