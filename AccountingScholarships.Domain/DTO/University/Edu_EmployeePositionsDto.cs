namespace AccountingScholarships.Domain.DTO.University;

public class Edu_EmployeePositionsDto
{
    public int ID { get; set; }
    public DateOnly StartedOn { get; set; }
    public DateOnly? EndedOn { get; set; }
    public string? LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public double? Rate { get; set; }
    public bool? IsMainPosition { get; set; }
    public int? HrOrderId { get; set; }
    public int OrgUnitID { get; set; }
    public int PositionID { get; set; }
    public int EmployeeID { get; set; }
    public OrgUnitRefDto? OrgUnit { get; set; }
    public PositionRefDto? Position { get; set; }
    public EmployeeRefDto? Employee { get; set; }

    public class OrgUnitRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
    }

    public class PositionRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
    }

    public class EmployeeRefDto
    {
        public int ID { get; set; }
        public bool IsAdvisor { get; set; }
    }
}
