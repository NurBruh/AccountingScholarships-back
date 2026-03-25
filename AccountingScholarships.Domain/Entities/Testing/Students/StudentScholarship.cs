namespace AccountingScholarships.Domain.Entities.Testing.Students;

using AccountingScholarships.Domain.Common;
using AccountingScholarships.Domain.Entities.Testing.Scholarships;

public class StudentScholarship : BaseEntity
{
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int ScholarshipId { get; set; }
    public Scholarship Scholarship { get; set; } = null!;

    public string? Status { get; set; }
    public string? Reason { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
