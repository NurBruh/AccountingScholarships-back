using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_SemesterCourses
    {
        public int ID { get; set; } // PK
        public int SemesterID { get; set; } // FK
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int OrgUnitID { get; set; } // FK
        public decimal Credits { get; set; }
        public int EctsCredits { get; set; }
        public int ControlTypeID { get; set; } // FK
        public int CourseTypeID { get; set; } // FK
        public int? CourseDVOTypeID { get; set; }
        public decimal Lectures { get; set; }
        public decimal Practices { get; set; }
        public decimal Labs { get; set; }
        public int LecturesMinStudentCount { get; set; }
        public int PracticesMinStudentCount { get; set; }
        public int LabsMinStudentCount { get; set; }
        public int LecturesMaxStudentCount { get; set; }
        public int PracticesMaxStudentCount { get; set; }
        public int LabsMaxStudentCount { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int? LanguageID { get; set; } // FK
        public int? parID { get; set; }
        public int isGroup { get; set; }
        public int mainId { get; set; }
        public bool isNotCountInGpa { get; set; }
        public int? CourseTypeDvoId { get; set; } // FK
        public int? Hero_Subject_ID { get; set; }

        // Navigation Properties
        public Edu_Semesters Semester { get; set; }
        public Edu_OrgUnits OrgUnit { get; set; }
        public Edu_ControlTypes ControlType { get; set; }
        public Edu_CourseTypes CourseType { get; set; }
        public Edu_Languages? Language { get; set; }
        public Edu_CourseTypeDvo? CourseTypeDvo { get; set; }
    }
}
