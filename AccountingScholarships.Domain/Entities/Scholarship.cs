namespace AccountingScholarships.Domain.Entities;

using AccountingScholarships.Domain.Common;

public class Scholarship : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? LostDate { get; set; }
    public DateTime? OrderLostDate { get; set; }
    public DateTime? OrderCandidateDate { get; set; }
    public string? Notes { get; set; }
    public string? Conditions { get; set; }
    public bool IsActive { get; set; }

    public int? ScholarshipTypeId { get; set; }
    public ScholarshipType? ScholarshipTypeRef { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public ICollection<StudentScholarship> StudentScholarships { get; set; } = new List<StudentScholarship>();
}
