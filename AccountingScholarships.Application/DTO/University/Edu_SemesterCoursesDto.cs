namespace AccountingScholarships.Application.DTO.University;

public class Edu_SemesterCoursesDto
{
    public int ID { get; set; }
    public int SemesterID { get; set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public int OrgUnitID { get; set; }
    public decimal Credits { get; set; }
    public int EctsCredits { get; set; }
    public int ControlTypeID { get; set; }
    public int CourseTypeID { get; set; }
    public int? CourseDVOTypeID { get; set; }
    public decimal Lectures { get; set; }
    public decimal Practices { get; set; }
    public decimal Labs { get; set; }
    public string? LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public int? LanguageID { get; set; }
    public int? CourseTypeDvoId { get; set; }
    public SemesterRefDto? Semester { get; set; }
    public OrgUnitRefDto? OrgUnit { get; set; }
    public SimpleRefDto? ControlType { get; set; }
    public SimpleRefDto? CourseType { get; set; }
    public SimpleRefDto? Language { get; set; }
    public CourseTypeDvoRefDto? CourseTypeDvo { get; set; }

    public class SemesterRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public int StudyYear { get; set; }
    }

    public class OrgUnitRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
    }

    public class SimpleRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
    }

    public class CourseTypeDvoRefDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
    }
}
