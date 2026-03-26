using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public record GetAllEduEmployeePositionsQuery : IRequest<IReadOnlyList<Edu_EmployeePositionsDto>>;
