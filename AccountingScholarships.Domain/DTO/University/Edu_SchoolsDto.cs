namespace AccountingScholarships.Domain.DTO.University;

public class Edu_SchoolsDto
{
    public int ID { get; set; }
    public int? SchoolTypeID { get; set; }
    public int? SchoolRegionStatusID { get; set; }
    public int? LocalityID { get; set; }
    public string? Number { get; set; }
    public string? Title { get; set; }
    public string? ShortTitle { get; set; }
}
