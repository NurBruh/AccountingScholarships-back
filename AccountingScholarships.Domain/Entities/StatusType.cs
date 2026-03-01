namespace AccountingScholarships.Domain.Entities;

public class StatusType
{
    public int Id { get; set; }
    public string StatusName { get; set; } = string.Empty;

    public ICollection<StatusStudentHistory> StatusHistories { get; set; } = new List<StatusStudentHistory>();
}
