using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public record GetEduEmployeePositionByIdQuery(int Id) : IRequest<Edu_EmployeePositionsDto?>;
