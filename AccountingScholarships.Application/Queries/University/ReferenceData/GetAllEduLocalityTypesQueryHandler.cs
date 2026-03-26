using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduLocalityTypesQueryHandler : IRequestHandler<GetAllEduLocalityTypesQuery, IReadOnlyList<Edu_LocalityTypesDto>>
{
    private readonly ISsoRepository<Edu_LocalityTypes> _repository;

    public GetAllEduLocalityTypesQueryHandler(ISsoRepository<Edu_LocalityTypes> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_LocalityTypesDto>> Handle(GetAllEduLocalityTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_LocalityTypesDto
            {
                ID = e.ID,
                Title = e.Title
            })
            .ToList()
            .AsReadOnly();
    }
}
