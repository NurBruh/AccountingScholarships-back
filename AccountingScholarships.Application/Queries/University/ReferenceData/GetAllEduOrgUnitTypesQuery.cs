using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduOrgUnitTypesQuery : IRequest<IReadOnlyList<Edu_OrgUnitTypesDto>>;
