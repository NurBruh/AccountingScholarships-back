namespace AccountingScholarships.Domain.DTO.University;

public class Edu_OrgUnitsDto
{
    public int ID { get; set; }
    public int? ParentID { get; set; }
    public string? Title { get; set; }
    public bool Deleted { get; set; }
    public string? ShortTitle { get; set; }
    public int TypeID { get; set; }

    public Edu_OrgUnitTypesDto? Type { get; set; }
    public ParentOrgUnitDto? Parent { get; set; }

    public class ParentOrgUnitDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public int? ParentID { get; set; }
    }
}
