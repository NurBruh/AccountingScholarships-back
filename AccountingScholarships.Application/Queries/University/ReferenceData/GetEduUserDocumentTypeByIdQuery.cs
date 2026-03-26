using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetEduUserDocumentTypeByIdQuery(int Id) : IRequest<Edu_UserDocumentTypesDto?>;
