namespace AccountingScholarships.Domain.Entities.Testing.Students;

using AccountingScholarships.Domain.Common;
using AccountingScholarships.Domain.Entities.Testing.StudentData;
using AccountingScholarships.Domain.Entities.Testing.Grants;
using AccountingScholarships.Domain.Entities.Testing.Scholarships;
using AccountingScholarships.Domain.Entities.Testing.Reference;

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
    public int Course { get; set; }
    public string iban { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string? Description { get; set; }
    public string? Sex { get; set; }
    public string? CreatedBy { get; set; }

    // FK → справочники
    public int? SpecialityId { get; set; }
    public Speciality? Speciality { get; set; }

    public int? StudyFormId { get; set; }
    public StudyForm? StudyForm { get; set; }

    public int? DegreeLevelId { get; set; }
    public DegreeLevel? DegreeLevel { get; set; }

    public int? BankId { get; set; }
    public Bank? Bank { get; set; }

    // Навигационные коллекции
    public ICollection<Grant> Grants { get; set; } = new List<Grant>();
    public ICollection<Scholarship> Scholarships { get; set; } = new List<Scholarship>();
    public ICollection<StudentGrant> StudentGrants { get; set; } = new List<StudentGrant>();
    public ICollection<StudentScholarship> StudentScholarships { get; set; } = new List<StudentScholarship>();
    public ICollection<StatusStudentHistory> StatusHistories { get; set; } = new List<StatusStudentHistory>();
}
