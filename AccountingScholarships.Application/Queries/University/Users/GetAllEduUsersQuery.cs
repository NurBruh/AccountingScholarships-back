using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public record GetAllEduUsersQuery : IRequest<IReadOnlyList<Edu_UsersDto>>;
