using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetEduSpecialityLevelByIdQueryHandler : IRequestHandler<GetEduSpecialityLevelByIdQuery, Edu_SpecialityLevelsDto?>
{
    private readonly ISsoRepository<Edu_SpecialityLevels> _repository;

    public GetEduSpecialityLevelByIdQueryHandler(ISsoRepository<Edu_SpecialityLevels> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_SpecialityLevelsDto?> Handle(GetEduSpecialityLevelByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_SpecialityLevelsDto { ID = entity.ID, Title = entity.Title, NoBDID = entity.NoBDID };
    }
}
