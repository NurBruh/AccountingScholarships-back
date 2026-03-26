using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public record GetEduEntrantByIdQuery(int Id) : IRequest<Edu_EntrantsDto?>;
