using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public class GetEduOrgUnitByIdQueryHandler : IRequestHandler<GetEduOrgUnitByIdQuery, Edu_OrgUnitsDto?>
{
    private readonly ISsoRepository<Edu_OrgUnits> _repository;

    public GetEduOrgUnitByIdQueryHandler(ISsoRepository<Edu_OrgUnits> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_OrgUnitsDto?> Handle(GetEduOrgUnitByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.FindFirstWithIncludesAsync(x => x.ID == request.Id, new[] { "Type", "Parent" }, cancellationToken);
        if (e is null) return null;

        return new Edu_OrgUnitsDto
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
        };
    }
}
