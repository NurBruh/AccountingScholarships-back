using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public record GetEduEmployeeByIdQuery(int Id) : IRequest<Edu_EmployeesDto?>;
