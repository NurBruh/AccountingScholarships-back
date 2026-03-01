using AccountingScholarships.Domain.Common;

namespace AccountingScholarships.Domain.Entities;

public class StatusStudentHistory : BaseEntity
{
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int StatusTypeId { get; set; }
    public StatusType StatusType { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Note { get; set; }
}
