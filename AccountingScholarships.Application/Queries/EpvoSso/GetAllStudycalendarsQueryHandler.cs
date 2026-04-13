using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
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
            StudyCalendarId = s.StudyCalendarId,
            UniversityId = s.UniversityId,
            Name = s.Name,
            StudyFormId = s.StudyFormId,
            Year = s.Year,
            CalendarTypeId = s.CalendarTypeId,
            ProfessionId = s.ProfessionId,
            SpecializationId = s.SpecializationId,
            Status = s.Status,
            EntranceYear = s.EntranceYear,
            TypeCode = s.TypeCode,
        }).ToList().AsReadOnly();
    }
}
