using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public record GetEduUserDocumentByIdQuery(int Id) : IRequest<Edu_UserDocumentsDto?>;
