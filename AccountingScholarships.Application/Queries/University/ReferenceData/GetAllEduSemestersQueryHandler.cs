using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
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
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_SemestersDto
            {
                ID = e.ID,
                Title = e.Title,
                StartsOn = e.StartsOn,
                EndsOn = e.EndsOn,
                StudyYear = e.StudyYear,
                SemesterTypeID = e.SemesterTypeID
            })
            .ToList()
            .AsReadOnly();
    }
}
