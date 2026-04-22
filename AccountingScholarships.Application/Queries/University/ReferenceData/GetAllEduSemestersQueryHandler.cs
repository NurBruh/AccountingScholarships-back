using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduSemestersQueryHandler : IRequestHandler<GetAllEduSemestersQuery, IReadOnlyList<Edu_SemestersDto>>
{
    private readonly ISsoRepository<Edu_Semesters> _repository;

    public GetAllEduSemestersQueryHandler(ISsoRepository<Edu_Semesters> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_SemestersDto>> Handle(GetAllEduSemestersQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllWithIncludesAsync(new[] { "SemesterType" }, cancellationToken);

        return entities
            .Select(e => new Edu_SemestersDto
            {
                ID = e.ID,
                Title = e.Title,
                StartsOn = e.StartsOn,
                EndsOn = e.EndsOn,
                StudyYear = e.StudyYear,
                SemesterTypeID = e.SemesterTypeID,
                SemesterType = e.SemesterType == null ? null : new Edu_SemesterTypesDto
                {
                    ID = e.SemesterType.ID,
                    Title = e.SemesterType.Title,
                    OrderBy = e.SemesterType.OrderBy
                }
            })
            .ToList()
            .AsReadOnly();
    }
}
