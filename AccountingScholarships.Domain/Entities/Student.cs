namespace AccountingScholarships.Domain.Entities;

using AccountingScholarships.Domain.Common;

public class Student : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string IIN { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? GroupName { get; set; }
    public string? Faculty { get; set; }
    public string? Speciality { get; set; }
    public int Course { get; set; }
    public string? EducationForm { get; set; }
    public string iban { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<Grant> Grants { get; set; } = new List<Grant>();
    public ICollection<Scholarship> Scholarships { get; set; } = new List<Scholarship>();
}
