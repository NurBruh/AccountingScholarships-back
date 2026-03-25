namespace AccountingScholarships.Domain.Entities.Testing.Reference;

using AccountingScholarships.Domain.Entities.Testing.Students;

public class StatusType
{
    public int Id { get; set; }
    public string StatusName { get; set; } = string.Empty;

    public ICollection<StatusStudentHistory> StatusHistories { get; set; } = new List<StatusStudentHistory>();
}
