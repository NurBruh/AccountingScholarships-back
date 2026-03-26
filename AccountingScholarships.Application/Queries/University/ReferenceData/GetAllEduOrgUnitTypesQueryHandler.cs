using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduOrgUnitTypesQueryHandler : IRequestHandler<GetAllEduOrgUnitTypesQuery, IReadOnlyList<Edu_OrgUnitTypesDto>>
{
    private readonly ISsoRepository<Edu_OrgUnitTypes> _repository;

    public GetAllEduOrgUnitTypesQueryHandler(ISsoRepository<Edu_OrgUnitTypes> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_OrgUnitTypesDto>> Handle(GetAllEduOrgUnitTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_OrgUnitTypesDto { ID = e.ID, Title = e.Title }).ToList().AsReadOnly();
    }
}
