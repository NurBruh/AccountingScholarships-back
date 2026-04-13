using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public record GetEduUserByIdQuery(int Id) : IRequest<Edu_UsersDto?>;
