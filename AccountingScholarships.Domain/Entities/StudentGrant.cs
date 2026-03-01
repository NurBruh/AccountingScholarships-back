using AccountingScholarships.Domain.Common;

namespace AccountingScholarships.Domain.Entities;

public class StudentGrant : BaseEntity
{
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int GrantId { get; set; }
    public Grant Grant { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Reason { get; set; }
}
