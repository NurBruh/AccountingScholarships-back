namespace AccountingScholarships.Domain.Entities.Testing.StudentData;

public class Institute
{
    public int Id { get; set; }
    public string InstituteName { get; set; } = string.Empty;
    public string? InstituteDirector { get; set; }

    public ICollection<Department> Departments { get; set; } = new List<Department>();
}
