using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllStudycalendarsQueryHandler
    : IRequestHandler<GetAllStudycalendarsQuery, IReadOnlyList<EpvoStudycalendarDto>>
{
    private readonly IEpvoSsoRepository<Studycalendar> _repository;

    public GetAllStudycalendarsQueryHandler(IEpvoSsoRepository<Studycalendar> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EpvoStudycalendarDto>> Handle(
        GetAllStudycalendarsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new EpvoStudycalendarDto
        {
            StudyCalendarId = s.studyCalendarId,
            UniversityId = s.UniversityId,
            Name = s.Name,
            StudyFormId = s.StudyFormId,
            Year = s.Year,
            CalendarTypeId = s.CalendarTypeId,
            ProfessionId = s.ProfessionId,
            Specialization = s.Specialization,
            Status = s.Status,
            EntranceYear = s.EntranceYear,
            TypeCode = s.TypeCode,
        }).ToList().AsReadOnly();
    }
}
