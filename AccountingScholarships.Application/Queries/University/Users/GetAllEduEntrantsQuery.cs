using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public record GetAllEduEntrantsQuery : IRequest<IReadOnlyList<Edu_EntrantsDto>>;
