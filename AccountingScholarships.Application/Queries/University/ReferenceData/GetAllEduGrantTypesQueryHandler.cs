using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetAllEduGrantTypesQueryHandler : IRequestHandler<GetAllEduGrantTypesQuery, IReadOnlyList<Edu_GrantTypesDto>>
{
    private readonly ISsoRepository<Edu_GrantTypes> _repository;
    public GetAllEduGrantTypesQueryHandler(ISsoRepository<Edu_GrantTypes> repository) { _repository = repository; }
    public async Task<IReadOnlyList<Edu_GrantTypesDto>> Handle(GetAllEduGrantTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_GrantTypesDto { ID = e.ID, Title = e.Title, ESUVOGrantTypeId = e.ESUVOGrantTypeId, Deleted = e.Deleted }).ToList().AsReadOnly();
    }
}
