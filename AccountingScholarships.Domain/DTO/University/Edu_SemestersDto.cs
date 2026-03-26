namespace AccountingScholarships.Domain.DTO.University;

public class Edu_SemestersDto
{
    public int ID { get; set; }
    public string? Title { get; set; }
    public DateTime? StartsOn { get; set; }
    public DateTime? EndsOn { get; set; }
    public int? StudyYear { get; set; }
    public int? SemesterTypeID { get; set; }

    // Navigation property
    public Edu_SemesterTypesDto? SemesterType { get; set; }
}
