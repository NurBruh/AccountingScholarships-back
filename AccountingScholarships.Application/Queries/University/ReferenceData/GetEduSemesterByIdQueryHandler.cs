using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduSemesterByIdQueryHandler : IRequestHandler<GetEduSemesterByIdQuery, Edu_SemestersDto?>
{
    private readonly ISsoRepository<Edu_Semesters> _repository;
    public GetEduSemesterByIdQueryHandler(ISsoRepository<Edu_Semesters> repository) { _repository = repository; }
    public async Task<Edu_SemestersDto?> Handle(GetEduSemesterByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.FindFirstWithIncludesAsync(x => x.ID == request.Id, new[] { "SemesterType" }, cancellationToken);
        if (e is null) return null;
        return new Edu_SemestersDto
        {
            ID = e.ID,
            Title = e.Title,
            StartsOn = e.StartsOn,
            EndsOn = e.EndsOn,
            StudyYear = e.StudyYear,
            SemesterTypeID = e.SemesterTypeID,
            SemesterType = e.SemesterType == null ? null : new Edu_SemesterTypesDto { ID = e.SemesterType.ID, Title = e.SemesterType.Title, OrderBy = e.SemesterType.OrderBy }
        };
    }
}
