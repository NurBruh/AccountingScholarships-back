using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.university
{
    public class Edu_Specialities
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int? YearsOfStudy { get; set; }
        public bool? Diaspora { get; set; }
        public bool? VillageQuota { get; set; }
        public bool Deleted { get; set; }
        public string? ShortTitle { get; set; }
        public string? Description { get; set; }
        public int? ESUVOID { get; set; }
        public bool? Classifier { get; set; }
        public int? EducationalProgramStatus { get; set; }
        public int? EducationalProgramType { get; set; }
        public int? Classifier2 { get; set; }
        public string? NoBDID { get; set; }
        public bool ReadyToSendESUVO { get; set; }
        public bool? DoubleDiploma { get; set; }
        public bool? Jointep { get; set; }
        public bool? Is_interdisciplinary { get; set; }
        public bool? is_not_active { get; set; }
        public decimal? EducationDuration { get; set; }
        public bool? isPrikladnoy { get; set; }

        // FK свойства
        public int LevelID { get; set; }
        public int? PrimarySubjectID { get; set; }
        public int? FithSubjectID { get; set; }
        public int? RupEditorOrgUnitID { get; set; }

        // Navigation Properties
        public EduSpecialityLevels Level { get; set; }
        public Edu_SchoolSubjects? PrimarySubject { get; set; }
        public Edu_SchoolSubjects? FithSubject { get; set; }
        public Edu_OrgUnits? RupEditorOrgUnit { get; set; }

        // Обратная связь
        public ICollection<EduStudents> Students { get; set; } = new List<EduStudents>();
        public ICollection<Edu_Rups> Rups { get; set; } = new List<Edu_Rups>();
    }
}
