namespace AccountingScholarships.Application.DTO;

public class ScholarshipLossRecordDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string IIN { get; set; } = string.Empty;
    public DateTime LostDate { get; set; }
    public string? OrderNumber { get; set; }
    public DateTime? OrderDate { get; set; }
    public string? Reason { get; set; }
    public string? ScholarshipName { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateScholarshipLossRecordDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string IIN { get; set; } = string.Empty;
    public DateTime LostDate { get; set; }
    public string? OrderNumber { get; set; }
    public DateTime? OrderDate { get; set; }
    public string? Reason { get; set; }
    public string? ScholarshipName { get; set; }
}
