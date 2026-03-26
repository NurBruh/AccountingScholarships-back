namespace AccountingScholarships.Domain.DTO.University;

public class Edu_EntrantsDto
{
    public int EntrantID { get; set; }
    public DateTime RegisteredOn { get; set; }
    public int? LevelID { get; set; }
    public int StatusID { get; set; }
    public bool? CheckedByAdmissions { get; set; }
    public bool AllowCheckByDoctor { get; set; }
    public bool? CheckedByDoctor { get; set; }
    public string? FormState { get; set; }
    public string? LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public bool? HasAppealation { get; set; }
    public bool? Application { get; set; }
    public bool? HasReceipt { get; set; }
    public DateTime? EnrollmentDate { get; set; }
    public UserRefDto? User { get; set; }
    public LevelRefDto? Level { get; set; }
    public StatusRefDto? Status { get; set; }

    public class UserRefDto
    {
        public int ID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? IIN { get; set; }
        public DateOnly? DOB { get; set; }
    }

    public class LevelRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
    }

    public class StatusRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
    }
}
