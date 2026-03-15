namespace AccountingScholarships.Domain.Entities.Reference;

using AccountingScholarships.Domain.Entities.Students;

public class Bank
{
    public int Id { get; set; }
    public string RecipientBank { get; set; } = string.Empty;
    public string Bic { get; set; } = string.Empty;

    public ICollection<Student> Students { get; set; } = new List<Student>();
}
