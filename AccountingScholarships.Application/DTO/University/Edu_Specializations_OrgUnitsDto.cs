namespace AccountingScholarships.Application.DTO.University;

public class Edu_Specializations_OrgUnitsDto
{
    public int? SpecializationID { get; set; }
    public int? OrgUnitID { get; set; }
    public SpecializationRefDto? Specialization { get; set; }
    public OrgUnitRefDto? OrgUnit { get; set; }

    public class SpecializationRefDto
    {
        public int Id { get; set; }
        public string? TitleRu { get; set; }
        public string? Code { get; set; }
    }

    public class OrgUnitRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
    }
}
