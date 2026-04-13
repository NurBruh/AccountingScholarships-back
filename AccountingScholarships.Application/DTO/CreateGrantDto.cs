namespace AccountingScholarships.Application.DTO;

public class CreateGrantDto
{
    public string Name { get; set; } = string.Empty;
    public string? Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public int StudentId { get; set; }
}
