using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public class GetAllEduOrgUnitsQueryHandler : IRequestHandler<GetAllEduOrgUnitsQuery, IReadOnlyList<Edu_OrgUnitsDto>>
{
    private readonly ISsoRepository<Edu_OrgUnits> _repository;

    public GetAllEduOrgUnitsQueryHandler(ISsoRepository<Edu_OrgUnits> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_OrgUnitsDto>> Handle(GetAllEduOrgUnitsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllWithIncludesAsync(new[] { "Type", "Parent" }, cancellationToken);

        return entities.Select(e => new Edu_OrgUnitsDto
        {
            ID = e.ID,
            ParentID = e.ParentID,
            Title = e.Title,
            Deleted = e.Deleted,
            ShortTitle = e.ShortTitle,
            TypeID = e.TypeID,
            Type = e.Type == null ? null : new Edu_OrgUnitTypesDto
            {
                ID = e.Type.ID,
                Title = e.Type.Title
            },
            Parent = e.Parent == null ? null : new Edu_OrgUnitsDto.ParentOrgUnitDto
            {
                ID = e.Parent.ID,
                Title = e.Parent.Title,
                ParentID = e.Parent.ParentID
            }
        }).ToList().AsReadOnly();
    }
}
