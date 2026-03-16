namespace AccountingScholarships.Domain.Entities.Grants;

using AccountingScholarships.Domain.Common;
using AccountingScholarships.Domain.Entities.Students;

public class Grant : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Type { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public string? Conditions { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; } = true;

    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public ICollection<StudentGrant> StudentGrants { get; set; } = new List<StudentGrant>();
}
