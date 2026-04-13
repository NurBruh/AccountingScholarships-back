using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetEduOrgUnitTypeByIdQueryHandler : IRequestHandler<GetEduOrgUnitTypeByIdQuery, Edu_OrgUnitTypesDto?>
{
    private readonly ISsoRepository<Edu_OrgUnitTypes> _repository;

    public GetEduOrgUnitTypeByIdQueryHandler(ISsoRepository<Edu_OrgUnitTypes> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_OrgUnitTypesDto?> Handle(GetEduOrgUnitTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_OrgUnitTypesDto { ID = entity.ID, Title = entity.Title };
    }
}
