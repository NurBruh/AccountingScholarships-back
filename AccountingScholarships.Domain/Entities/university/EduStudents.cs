using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.university
{
    public class EduStudents
    {
        public int StudentID { get; set; } // PK, FK -> EduUsers.ID
        public int? SpecialityID { get; set; }
        public int? StatusID { get; set; }
        public int? CategoryID { get; set; }
        public bool NeedsDorm { get; set; }
        public bool AltynBelgi { get; set; }
        public int Year { get; set; }
        public int? RupID { get; set; }
        public DateOnly? EntryDate { get; set; }
        public double? GPA { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public DateTime? GraduatedOn { get; set; }
        public DateOnly? AcademicStatusEndsOn { get; set; }
        public DateOnly? AcademicStatusStartsOn { get; set; }
        public double? GPA_Y { get; set; }
        public bool? IsPersonalDataComplete { get; set; }
        public int? HosterPrivelegeID { get; set; }
        public int? MinorSpecialityID { get; set; }
        public int? EnrollmentTypeID { get; set; }
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

        // FK свойства
        public int? EducationTypeID { get; set; }
        public int? EducationPaymentTypeID { get; set; }
        public int? GrantTypeID { get; set; }
        public int? EducationDurationID { get; set; }
        public int? StudyLanguageID { get; set; }
        public int? AcademicStatusID { get; set; }
        public int? AdvisorID { get; set; }

        // Navigation Properties
        public EduUsers User { get; set; }
        public EduEducationTypes? EducationType { get; set; }
        public EduEducationPaymentTypes? EducationPaymentType { get; set; }
        public EduGrantTypes? GrantType { get; set; }
        public EduEducationDurations? EducationDuration { get; set; }
        public EduLanguages? StudyLanguage { get; set; }
        public EduAcademicStatuses? AcademicStatus { get; set; }
        public EduEmployees? Advisor { get; set; }
    }
}
