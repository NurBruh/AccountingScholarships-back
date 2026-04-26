using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_Students
    {
        public int StudentID { get; set; } // PK, FK -> Edu_Users.ID
        public int? SpecialityID { get; set; }
        public int? StatusID { get; set; }
        public int? CategoryID { get; set; }
        public bool NeedsDorm { get; set; }
        public bool AltynBelgi { get; set; }
        public int Year { get; set; }
        public int? RupID { get; set; }
        public DateOnly? EntryDate { get; set; }
        public float? GPA { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public DateTime? GraduatedOn { get; set; }
        public DateOnly? AcademicStatusEndsOn { get; set; }
        public DateOnly? AcademicStatusStartsOn { get; set; }
        public float? GPA_Y { get; set; }
        public bool? IsPersonalDataComplete { get; set; }
        public int? HosterPrivelegeID { get; set; }
        public int? MinorSpecialityID { get; set; }
        public int? EnrollmentTypeId { get; set; }
        public float? EctsGPA { get; set; }
        public float? EctsGPA_Y { get; set; }
        public bool? IsScholarship { get; set; }
        public int? ScholarshipTypeID { get; set; }
        public string? ScholarshipOrderNumber { get; set; }
        public DateOnly? ScholarshipOrderDate { get; set; }
        public DateOnly? ScholarshipDateStart { get; set; }
        public DateOnly? ScholarshipDateEnd { get; set; }
        public int? FundingID { get; set; }
        public bool? IsKNB { get; set; }

        // FK свойства
        public int? EducationTypeID { get; set; }
        public int? EducationPaymentTypeID { get; set; }
        public int? GrantTypeID { get; set; }
        public int? EducationDurationID { get; set; }
        public int? StudyLanguageID { get; set; }
        public int? AcademicStatusID { get; set; }
        public int? AdvisorID { get; set; }

        // Navigation Properties
        public Edu_Users User { get; set; }
        public Edu_EducationTypes? EducationType { get; set; }
        public Edu_EducationPaymentTypes? EducationPaymentType { get; set; }
        public Edu_GrantTypes? GrantType { get; set; }
        public Edu_EducationDurations? EducationDuration { get; set; }
        public Edu_Languages? StudyLanguage { get; set; }
        public Edu_AcademicStatuses? AcademicStatus { get; set; }
        public Edu_Employees? Advisor { get; set; }
        public Edu_Specialities? Speciality { get; set; }
        public Edu_Rups? Rup { get; set; }
        public Edu_StudentStatuses? Status { get; set; }
        public Edu_StudentCategories? Category { get; set; }
    }
}
