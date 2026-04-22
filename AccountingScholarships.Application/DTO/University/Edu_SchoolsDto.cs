namespace AccountingScholarships.Application.DTO.University;

public class Edu_SchoolsDto
{
    public int ID { get; set; }
    public int? SchoolTypeID { get; set; }
    public int? SchoolRegionStatusID { get; set; }
    public int? LocalityID { get; set; }
    public string? Number { get; set; }
    public string? Title { get; set; }
    public string? ShortTitle { get; set; }

    // Navigation properties
    public SchoolTypeRefDto? SchoolType { get; set; }
    public SchoolRegionStatusRefDto? SchoolRegionStatus { get; set; }
    public LocalityRefDto? Locality { get; set; }

    public class SchoolTypeRefDto { public int ID { get; set; } public string? Title { get; set; } }
    public class SchoolRegionStatusRefDto { public int ID { get; set; } public string? Title { get; set; } }
    public class LocalityRefDto { public int ID { get; set; } public string? Title { get; set; } public int? ParentID { get; set; } public string? ESUVOCenterKatoCode { get; set; } }
}
