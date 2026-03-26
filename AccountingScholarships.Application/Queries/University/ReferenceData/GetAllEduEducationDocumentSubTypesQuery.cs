using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduEducationDocumentSubTypesQuery : IRequest<IReadOnlyList<Edu_EducationDocumentSubTypesDto>>;
