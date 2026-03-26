using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduSpecialityLevelsQueryHandler : IRequestHandler<GetAllEduSpecialityLevelsQuery, IReadOnlyList<Edu_SpecialityLevelsDto>>
{
    private readonly ISsoRepository<Edu_SpecialityLevels> _repository;

    public GetAllEduSpecialityLevelsQueryHandler(ISsoRepository<Edu_SpecialityLevels> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_SpecialityLevelsDto>> Handle(GetAllEduSpecialityLevelsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_SpecialityLevelsDto { ID = e.ID, Title = e.Title, NoBDID = e.NoBDID }).ToList().AsReadOnly();
    }
}
