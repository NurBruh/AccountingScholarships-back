namespace AccountingScholarships.Domain.DTO;

public class StudentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string IIN { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? GroupName { get; set; }
    public int Course { get; set; }
    public bool IsActive { get; set; }
    public string iban { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Sex { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Resolved names (для обратной совместимости)
    public string? Faculty { get; set; }
    public string? Speciality { get; set; }
    public string? EducationForm { get; set; }
    public string? DegreeLevel { get; set; }
    public string? BankName { get; set; }
    public string? DepartmentName { get; set; }

    // FK IDs
    public int? SpecialityId { get; set; }
    public int? StudyFormId { get; set; }
    public int? DegreeLevelId { get; set; }
    public int? BankId { get; set; }

    public List<GrantDto> Grants { get; set; } = new();
    public List<ScholarshipDto> Scholarships { get; set; } = new();
}
