namespace AccountingScholarships.Domain.DTO;

public class EpvoStudentDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string IIN { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? Faculty { get; set; }
    public string? Speciality { get; set; }
    public int Course { get; set; }
    public string? GrantName { get; set; }
    public decimal? GrantAmount { get; set; }
    public string? ScholarshipName { get; set; }
    public decimal? ScholarshipAmount { get; set; }
    public bool IsActive { get; set; }
    public DateTime SyncDate { get; set; }
}
