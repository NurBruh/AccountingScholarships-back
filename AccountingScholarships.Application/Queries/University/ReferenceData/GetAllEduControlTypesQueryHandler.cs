using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduControlTypesQueryHandler : IRequestHandler<GetAllEduControlTypesQuery, IReadOnlyList<Edu_ControlTypesDto>>
{
    private readonly ISsoRepository<Edu_ControlTypes> _repository;

    public GetAllEduControlTypesQueryHandler(ISsoRepository<Edu_ControlTypes> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_ControlTypesDto>> Handle(GetAllEduControlTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_ControlTypesDto
            {
                ID = e.ID,
                Title = e.Title,
                ShortTitle = e.ShortTitle
            })
            .ToList()
            .AsReadOnly();
    }
}
