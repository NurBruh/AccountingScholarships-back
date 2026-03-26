using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public record GetAllEduEmployeesQuery : IRequest<IReadOnlyList<Edu_EmployeesDto>>;
