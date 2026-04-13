namespace AccountingScholarships.Application.DTO;

public class UpdateScholarshipDto
{
    public string Name { get; set; } = string.Empty;
    public string? Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    //public DateTime? EndDate { get; set; }
    public DateTime? LostDate { get; set; }
    public DateTime? OrderLostDate { get; set; }
    public DateTime? OrderCandidateDate { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
}
