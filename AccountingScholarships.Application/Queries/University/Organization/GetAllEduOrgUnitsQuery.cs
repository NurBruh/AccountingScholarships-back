using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public record GetAllEduOrgUnitsQuery : IRequest<IReadOnlyList<Edu_OrgUnitsDto>>;
