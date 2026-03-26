using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_StudentCourses
    {
        public int ID { get; set; } // PK
        public int StudentID { get; set; } // FK
        public int SemesterCourseID { get; set; } // FK
        public string RegisteredBy { get; set; }
        public DateTime RegisteredOn { get; set; }
        public double? Grade1 { get; set; }
        public double? Grade2 { get; set; }
        public double? ExamGrade { get; set; }
        public string? LetterGrade { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string? ExtraGrade { get; set; }
        public int? LevelID { get; set; } // FK
        public int? CourseAttributeID { get; set; }
        public int? prevID { get; set; }
        public int? MissingPercentage { get; set; }
        public bool MissingFailure { get; set; }
        public double? UnsubmittedGrade1 { get; set; }
        public double? UnsubmittedGrade2 { get; set; }
        public bool Transfer { get; set; }
        public bool? Ido { get; set; }
        public int? IdoSemester { get; set; }
        public double? ExtraExamGrade { get; set; }

        // Navigation Properties
        public Edu_Students Student { get; set; }
        public Edu_SemesterCourses SemesterCourse { get; set; }
        public Edu_SpecialityLevels? Level { get; set; }
    }
}
