using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public record GetAllEduEmployeePositionsQuery : IRequest<IReadOnlyList<Edu_EmployeePositionsDto>>;
