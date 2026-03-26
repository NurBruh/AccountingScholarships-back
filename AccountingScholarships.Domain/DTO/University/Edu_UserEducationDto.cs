namespace AccountingScholarships.Domain.DTO.University;

public class Edu_UserEducationDto
{
    public int ID { get; set; }
    public int UserID { get; set; }
    public int? SchoolID { get; set; }
    public string? SchoolText { get; set; }
    public DateTime? GraduatedOn { get; set; }
    public int DocumentTypeID { get; set; }
    public int? DocumentSubTypeID { get; set; }
    public string? Number { get; set; }
    public string? Series { get; set; }
    public DateTime? IssuedOn { get; set; }
    public double? GPA { get; set; }
    public int? StudyLanguageID { get; set; }
    public string? ExtraInfo { get; set; }
    public int? SpecialityID { get; set; }
    public string? SpecialityText { get; set; }
    public string? Qualification { get; set; }
    public bool? IsSecondEducation { get; set; }
    public bool? IsRuralQuota { get; set; }
    public UserRefDto? User { get; set; }
    public SimpleRefDto? DocumentType { get; set; }
    public SimpleRefDto? DocumentSubType { get; set; }
    public SimpleRefDto? StudyLanguage { get; set; }
    public SpecialityRefDto? Speciality { get; set; }

    public class UserRefDto
    {
        public int ID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
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
