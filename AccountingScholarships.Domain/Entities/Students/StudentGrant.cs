namespace AccountingScholarships.Domain.Entities.Students;

using AccountingScholarships.Domain.Common;
using AccountingScholarships.Domain.Entities.Grants;

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
