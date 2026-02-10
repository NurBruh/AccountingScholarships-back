namespace AccountingScholarships.Application.DTOs;

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
    public string? Faculty { get; set; }
    public string? Speciality { get; set; }
    public int Course { get; set; }
    public string? EducationForm { get; set; }
}
