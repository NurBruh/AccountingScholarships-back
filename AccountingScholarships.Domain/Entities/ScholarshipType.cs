namespace AccountingScholarships.Domain.Entities;

public class ScholarshipType
{
    public int Id { get; set; }
    public string ScholarshipName { get; set; } = string.Empty;

    public ICollection<Scholarship> Scholarships { get; set; } = new List<Scholarship>();
}
