using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetStudycalendarByIdQueryHandler
    : IRequestHandler<GetStudycalendarByIdQuery, EpvoStudycalendarDto?>
{
    private readonly IEpvoSsoRepository<Studycalendar> _repository;

    public GetStudycalendarByIdQueryHandler(IEpvoSsoRepository<Studycalendar> repository)
    {
        _repository = repository;
    }

    public async Task<EpvoStudycalendarDto?> Handle(
        GetStudycalendarByIdQuery request, CancellationToken cancellationToken)
    {
        var s = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (s is null) return null;

        return new EpvoStudycalendarDto
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
        };
    }
}
