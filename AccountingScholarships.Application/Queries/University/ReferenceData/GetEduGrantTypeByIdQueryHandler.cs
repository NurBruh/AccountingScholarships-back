using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduGrantTypeByIdQueryHandler : IRequestHandler<GetEduGrantTypeByIdQuery, Edu_GrantTypesDto?>
{
    private readonly ISsoRepository<Edu_GrantTypes> _repository;
    public GetEduGrantTypeByIdQueryHandler(ISsoRepository<Edu_GrantTypes> repository) { _repository = repository; }
    public async Task<Edu_GrantTypesDto?> Handle(GetEduGrantTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_GrantTypesDto { ID = entity.ID, Title = entity.Title, ESUVOGrantTypeId = entity.ESUVOGrantTypeId, Deleted = entity.Deleted };
    }
}
