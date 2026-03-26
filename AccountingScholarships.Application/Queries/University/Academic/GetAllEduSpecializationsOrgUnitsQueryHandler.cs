using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public class GetAllEduSpecializationsOrgUnitsQueryHandler : IRequestHandler<GetAllEduSpecializationsOrgUnitsQuery, IReadOnlyList<Edu_Specializations_OrgUnitsDto>>
{
    private readonly ISsoRepository<Edu_Specializations_OrgUnits> _repository;

    public GetAllEduSpecializationsOrgUnitsQueryHandler(ISsoRepository<Edu_Specializations_OrgUnits> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_Specializations_OrgUnitsDto>> Handle(GetAllEduSpecializationsOrgUnitsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllWithIncludesAsync(new[] { "Specialization", "OrgUnit" }, cancellationToken);

        return entities.Select(e => new Edu_Specializations_OrgUnitsDto
        {
            SpecializationID = e.SpecializationID,
            OrgUnitID = e.OrgUnitID,
            Specialization = e.Specialization == null ? null : new Edu_Specializations_OrgUnitsDto.SpecializationRefDto
            {
                Id = e.Specialization.Id,
                TitleRu = e.Specialization.TitleRu,
                Code = e.Specialization.Code
            },
            OrgUnit = e.OrgUnit == null ? null : new Edu_Specializations_OrgUnitsDto.OrgUnitRefDto
            {
                ID = e.OrgUnit.ID,
                Title = e.OrgUnit.Title
            }
        }).ToList().AsReadOnly();
    }
}
