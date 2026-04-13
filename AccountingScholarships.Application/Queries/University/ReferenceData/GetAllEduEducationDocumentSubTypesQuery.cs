using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduEducationDocumentSubTypesQuery : IRequest<IReadOnlyList<Edu_EducationDocumentSubTypesDto>>;
