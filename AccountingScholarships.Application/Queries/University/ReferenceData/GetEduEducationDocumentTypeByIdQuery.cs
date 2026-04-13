using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduEducationDocumentTypeByIdQuery(int Id) : IRequest<Edu_EducationDocumentTypesDto?>;
