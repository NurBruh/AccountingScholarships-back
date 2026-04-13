namespace AccountingScholarships.Application.DTO.University;

public class StudentWithUserDto
{
    public int StudentID { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int? SpecialityID { get; set; }
    public int? StatusID { get; set; }
    public int? CategoryID { get; set; }
    public bool NeedsDorm { get; set; }
    public bool AltynBelgi { get; set; }
    public int? EducationTypeID { get; set; }
    public int? EducationPaymentTypeID { get; set; }
    public int? GrantTypeID { get; set; }
    public int? EducationDurationID { get; set; }
    public int Year { get; set; }
    public int? StudyLanguageID { get; set; }
    public int? RupID { get; set; }
    public DateOnly? EntryDate { get; set; }
    public double? GPA { get; set; }
    public string LastUpdatedBy { get; set; } = string.Empty;
    public DateTime LastUpdatedOn { get; set; }
    public int? AdvisorID { get; set; }
    public int? AcademicStatusID { get; set; }
    public DateTime? GraduatedOn { get; set; }
    public DateOnly? AcademicStatusEndsOn { get; set; }
    public DateOnly? AcademicStatusStartsOn { get; set; }
    public double? GPA_Y { get; set; }
    public bool? IsPersonalDataComplete { get; set; }
    public int? HosterPrivelegeID { get; set; }
    public int? MinorSpecialityID { get; set; }
    public int? EnrollmentTypeId { get; set; }
    public double? EctsGPA { get; set; }
    public double? EctsGPA_Y { get; set; }
    public bool? IsScholarship { get; set; }
    public int? ScholarshipTypeID { get; set; }
    public string? ScholarshipOrderNumber { get; set; }
    public DateOnly? ScholarshipOrderDate { get; set; }
    public DateOnly? ScholarshipDateStart { get; set; }
    public DateOnly? ScholarshipDateEnd { get; set; }
    public int? FundingID { get; set; }
    public bool? IsKNB { get; set; }

    // Navigation properties
    public UserRefDto? User { get; set; }
    public SimpleRefDto? EducationType { get; set; }
    public SimpleRefDto? EducationPaymentType { get; set; }
    public SimpleRefDto? GrantType { get; set; }
    public SimpleRefDto? EducationDuration { get; set; }
    public SimpleRefDto? StudyLanguage { get; set; }
    public SimpleRefDto? AcademicStatus { get; set; }
    public SpecialityRefDto? Speciality { get; set; }
    public SimpleRefDto? Status { get; set; }
    public SimpleRefDto? Category { get; set; }

    public class UserRefDto
    {
        public int ID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? Email { get; set; }
        public string? IIN { get; set; }
        public DateOnly? DOB { get; set; }
        public bool? Male { get; set; }
        public string? MobilePhone { get; set; }
        public string? HomePhone { get; set; }
        public bool Resident { get; set; }
    }

    public class SimpleRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
    }

    public class SpecialityRefDto
    {
        public int ID { get; set; }
        public string? Code { get; set; }
        public string? Title { get; set; }
    }
}
