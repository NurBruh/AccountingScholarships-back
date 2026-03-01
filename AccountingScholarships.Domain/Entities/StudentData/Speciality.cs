namespace AccountingScholarships.Domain.Entities.StudentData;

public class Speciality
{
    public int Id { get; set; }
    public string SpecialityName { get; set; } = string.Empty;

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public ICollection<Student> Students { get; set; } = new List<Student>();
}
