using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllObsoleteEduRegionsQueryHandler : IRequestHandler<GetAllObsoleteEduRegionsQuery, IReadOnlyList<Obsolete_Edu_RegionsDto>>
{
    private readonly ISsoRepository<Obsolete_Edu_Regions> _repository;

    public GetAllObsoleteEduRegionsQueryHandler(ISsoRepository<Obsolete_Edu_Regions> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Obsolete_Edu_RegionsDto>> Handle(GetAllObsoleteEduRegionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Obsolete_Edu_RegionsDto
            {
                ID = e.ID,
                Title = e.Title
            })
            .ToList()
            .AsReadOnly();
    }
}
