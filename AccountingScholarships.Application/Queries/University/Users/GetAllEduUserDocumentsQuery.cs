using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public record GetAllEduUserDocumentsQuery : IRequest<IReadOnlyList<Edu_UserDocumentsDto>>;
