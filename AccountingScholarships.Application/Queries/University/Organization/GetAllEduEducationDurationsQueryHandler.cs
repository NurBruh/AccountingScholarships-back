using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public class GetAllEduEducationDurationsQueryHandler : IRequestHandler<GetAllEduEducationDurationsQuery, IReadOnlyList<Edu_EducationDurationsDto>>
{
    private readonly ISsoRepository<Edu_EducationDurations> _repository;

    public GetAllEduEducationDurationsQueryHandler(ISsoRepository<Edu_EducationDurations> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_EducationDurationsDto>> Handle(GetAllEduEducationDurationsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllWithIncludesAsync(new[] { "Level" }, cancellationToken);

        return entities.Select(e => new Edu_EducationDurationsDto
        {
            ID = e.ID,
            Title = e.Title,
            ShortTitle = e.ShortTitle,
            NoBDIId = e.NoBDIId,
            LevelID = e.LevelID,
            Level = e.Level == null ? null : new Edu_SpecialityLevelsDto
            {
                ID = e.Level.ID,
                Title = e.Level.Title,
                NoBDID = e.Level.NoBDID
            }
        }).ToList().AsReadOnly();
    }
}
