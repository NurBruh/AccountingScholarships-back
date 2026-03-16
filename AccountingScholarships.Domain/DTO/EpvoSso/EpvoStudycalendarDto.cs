namespace AccountingScholarships.Domain.DTO.EpvoSso;

public class EpvoStudycalendarDto
{
    public int StudyCalendarId { get; set; }
    public int? UniversityId { get; set; }
    public string? Name { get; set; }
    public int? StudyFormId { get; set; }
    public int? Year { get; set; }
    public int? CalendarTypeId { get; set; }
    public int? ProfessionId { get; set; }
    public int? Specialization { get; set; }
    public int? Status { get; set; }
    public int? EntranceYear { get; set; }
    public string? TypeCode { get; set; }
}
