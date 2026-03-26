using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public record GetEduUserEducationByIdQuery(int Id) : IRequest<Edu_UserEducationDto?>;
