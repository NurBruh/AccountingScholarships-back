namespace AccountingScholarships.Domain.Entities;

using AccountingScholarships.Domain.Common;

public class Grant : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; } = true;

    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;
}
