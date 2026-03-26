using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduUserDocumentTypesQuery : IRequest<IReadOnlyList<Edu_UserDocumentTypesDto>>;
