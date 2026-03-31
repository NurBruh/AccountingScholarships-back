namespace AccountingScholarships.Domain.DTO.EpvoSso;

public class StudentSsoDetailDto
{
    public int? UniversityId { get; set; }
    public int StudentId { get; set; }
    public string? FullName { get; set; }
    public string? IinPlt { get; set; }
    public int? CourseNumber { get; set; }
    public string? StudyForm { get; set; }
    public string? PaymentType { get; set; }
    public decimal? Gpa { get; set; }
    public string? StudyLanguage { get; set; }
    public string? ProfessionName { get; set; }
    public string? Specialization { get; set; }
    public string? FacultyName { get; set; }
    public string? Sex { get; set; }
    public string? GrantType { get; set; }
    public string? Iic { get; set; }
}
