namespace AccountingScholarships.Domain.Entities.Testing.StudentData;

public class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string? DepartmentHead { get; set; }

    public int InstituteId { get; set; }
    public Institute Institute { get; set; } = null!;

    public ICollection<Speciality> Specialities { get; set; } = new List<Speciality>();
}
