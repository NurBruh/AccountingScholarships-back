using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduEducationDocumentTypesQuery : IRequest<IReadOnlyList<Edu_EducationDocumentTypesDto>>;
