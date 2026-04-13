using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public record GetAllEduSpecializationsOrgUnitsQuery : IRequest<IReadOnlyList<Edu_Specializations_OrgUnitsDto>>;
