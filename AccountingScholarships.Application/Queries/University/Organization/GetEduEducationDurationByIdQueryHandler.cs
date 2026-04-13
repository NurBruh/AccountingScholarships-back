using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public class GetEduEducationDurationByIdQueryHandler : IRequestHandler<GetEduEducationDurationByIdQuery, Edu_EducationDurationsDto?>
{
    private readonly ISsoRepository<Edu_EducationDurations> _repository;

    public GetEduEducationDurationByIdQueryHandler(ISsoRepository<Edu_EducationDurations> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_EducationDurationsDto?> Handle(GetEduEducationDurationByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.FindFirstWithIncludesAsync(x => x.ID == request.Id, new[] { "Level" }, cancellationToken);
        if (e is null) return null;

        return new Edu_EducationDurationsDto
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
        };
    }
}
