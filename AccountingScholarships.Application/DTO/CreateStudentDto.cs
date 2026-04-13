namespace AccountingScholarships.Application.DTO;

public class CreateStudentDto
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
    public string? Description { get; set; }
    public string? Sex { get; set; }

    // FK IDs (вместо строковых полей)
    public int? SpecialityId { get; set; }
    public int? StudyFormId { get; set; }
    public int? DegreeLevelId { get; set; }
    public int? BankId { get; set; }
}
