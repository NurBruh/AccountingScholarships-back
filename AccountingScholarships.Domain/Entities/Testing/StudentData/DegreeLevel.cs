using AccountingScholarships.Domain.Entities.Testing.Students;

namespace AccountingScholarships.Domain.Entities.Testing.StudentData;

public class DegreeLevel
{
    public int Id { get; set; }
    public string DegreeName { get; set; } = string.Empty;

    public ICollection<Student> Students { get; set; } = new List<Student>();
}
