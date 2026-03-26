namespace AccountingScholarships.Domain.DTO.University;

public class Edu_EducationDurationsDto
{
    public int ID { get; set; }
    public string? Title { get; set; }
    public string? ShortTitle { get; set; }
    public string? NoBDIId { get; set; }
    public int LevelID { get; set; }
    public Edu_SpecialityLevelsDto? Level { get; set; }
}
