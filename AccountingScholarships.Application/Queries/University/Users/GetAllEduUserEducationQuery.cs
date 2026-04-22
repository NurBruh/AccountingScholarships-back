using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public record GetAllEduUserEducationQuery : IRequest<IReadOnlyList<Edu_UserEducationDto>>;
