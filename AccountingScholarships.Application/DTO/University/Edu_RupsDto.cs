namespace AccountingScholarships.Application.DTO.University;

public class Edu_RupsDto
{
    public int ID { get; set; }
    public int SpecialityID { get; set; }
    public int? SpecialisationID { get; set; }
    public int Year { get; set; }
    public int SemesterCount { get; set; }
    public int? AlgorithmID { get; set; }
    public int? CreditsLimitId { get; set; }
    public bool IsModular { get; set; }
    public bool ApprovedByChair { get; set; }
    public string? ApprovedByChairUserID { get; set; }
    public DateTime? ApprovedByChairOn { get; set; }
    public bool ApprovedByOR { get; set; }
    public string? ApprovedByORUserID { get; set; }
    public DateTime? ApprovedByOROn { get; set; }
    public bool Locked { get; set; }
    public int? EducationDurationID { get; set; }
    public string? RejectionReason { get; set; }
    public string? LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public int? AcademicDegreeId { get; set; }
    public string? RupTitle { get; set; }
    public bool? IncludeToRegOp { get; set; }
    public bool? EducationalProgram { get; set; }
    public int? EducationalProgramId { get; set; }
    public bool? DualProgram { get; set; }

    public AlgorithmRefDto? Algorithm { get; set; }
    public EducationDurationRefDto? EducationDuration { get; set; }
    public SpecialityRefDto? Speciality { get; set; }

    public class AlgorithmRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
    }

    public class EducationDurationRefDto
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
