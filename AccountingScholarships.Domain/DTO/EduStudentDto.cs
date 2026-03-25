namespace AccountingScholarships.Domain.DTO;

public class EduStudentDto
{
    // Данные пользователя (Edu_Users)
    public int StudentID { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? IIN { get; set; }
    public DateOnly? DOB { get; set; }
    public bool? Male { get; set; }
    public bool Resident { get; set; }
    public string? MobilePhone { get; set; }

    // Академические данные (Edu_Students)
    public int Year { get; set; }
    public double? GPA { get; set; }
    public double? GPA_Y { get; set; }
    public double? EctsGPA { get; set; }
    public double? EctsGPA_Y { get; set; }
    public bool NeedsDorm { get; set; }
    public bool AltynBelgi { get; set; }
    public bool? IsScholarship { get; set; }
    public bool? IsKNB { get; set; }
    public DateOnly? EntryDate { get; set; }
    public DateTime? GraduatedOn { get; set; }
    public DateOnly? AcademicStatusEndsOn { get; set; }
    public DateOnly? AcademicStatusStartsOn { get; set; }

    // Стипендия
    public string? ScholarshipOrderNumber { get; set; }
    public DateOnly? ScholarshipOrderDate { get; set; }
    public DateOnly? ScholarshipDateStart { get; set; }
    public DateOnly? ScholarshipDateEnd { get; set; }

    // Resolved from FK (строковые названия)
    public string? EducationType { get; set; }
    public string? EducationPaymentType { get; set; }
    public string? GrantType { get; set; }
    public string? EducationDuration { get; set; }
    public string? StudyLanguage { get; set; }
    public string? AcademicStatus { get; set; }
    public string? AdvisorFullName { get; set; }
    public string? Nationality { get; set; }
    public string? CitizenshipCountry { get; set; }
}
