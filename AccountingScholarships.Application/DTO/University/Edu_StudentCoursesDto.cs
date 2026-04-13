namespace AccountingScholarships.Application.DTO.University;

public class Edu_StudentCoursesDto
{
    public int ID { get; set; }
    public int StudentID { get; set; }
    public int SemesterCourseID { get; set; }
    public string? RegisteredBy { get; set; }
    public DateTime RegisteredOn { get; set; }
    public double? Grade1 { get; set; }
    public double? Grade2 { get; set; }
    public double? ExamGrade { get; set; }
    public string? LetterGrade { get; set; }
    public string? LastUpdatedBy { get; set; }
    public DateTime LastUpdatedOn { get; set; }
    public string? ExtraGrade { get; set; }
    public int? LevelID { get; set; }
    public int? CourseAttributeID { get; set; }
    public int? MissingPercentage { get; set; }
    public bool MissingFailure { get; set; }
    public bool Transfer { get; set; }
    public bool? Ido { get; set; }
    public StudentRefDto? Student { get; set; }
    public SemesterCourseRefDto? SemesterCourse { get; set; }
    public LevelRefDto? Level { get; set; }

    public class StudentRefDto
    {
        public int StudentID { get; set; }
        public int Year { get; set; }
    }

    public class SemesterCourseRefDto
    {
        public int ID { get; set; }
        public string? Code { get; set; }
        public string? Title { get; set; }
    }

    public class LevelRefDto
    {
        public int ID { get; set; }
        public string? Title { get; set; }
    }
}
