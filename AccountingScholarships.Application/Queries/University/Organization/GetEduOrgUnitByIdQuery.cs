using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public record GetEduOrgUnitByIdQuery(int Id) : IRequest<Edu_OrgUnitsDto?>;
