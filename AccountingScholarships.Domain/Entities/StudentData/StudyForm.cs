namespace AccountingScholarships.Domain.Entities.StudentData;

public class StudyForm
{
    public int Id { get; set; }
    public string StudyFormName { get; set; } = string.Empty;

    public ICollection<Student> Students { get; set; } = new List<Student>();
}
