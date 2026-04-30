using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_UserEducation
    {
        public int ID { get; set; } // PK
        public int UserID { get; set; } // FK
        public int? SchoolID { get; set; } // FK
        public string? SchoolText { get; set; }
        public DateTime? GraduatedOn { get; set; }
        public int DocumentTypeID { get; set; } // FK
        public int? DocumentSubTypeID { get; set; } // FK
        public string? Number { get; set; }
        public string? Series { get; set; }
        public DateTime? IssuedOn { get; set; }
        public double? GPA { get; set; }
        public int? StudyLanguageID { get; set; } // FK
        public string? ExtraInfo { get; set; }
        public Guid? FileContainerID { get; set; }
        public int? SpecialityID { get; set; } // FK
        public string? SpecialityText { get; set; }
        public string? Qualification { get; set; }
        public bool? IsSecondEducation { get; set; }
        public bool? IsRuralQuota { get; set; }

        // Navigation Properties
        public Edu_Users User { get; set; }
        public Edu_EducationDocumentTypes DocumentType { get; set; }
        public Edu_EducationDocumentSubTypes? DocumentSubType { get; set; }
        public Edu_Languages? StudyLanguage { get; set; }
        public Edu_Specialities? Speciality { get; set; }
    }
}
