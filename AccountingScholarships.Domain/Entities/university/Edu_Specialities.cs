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
        //FK LevelID (FK, int, not null)
        public int LevelID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int? YearsOfStudy { get; set; }
        public bool? Diaspora { get; set; }
        public bool VillageQuota { get; set; }
        //FK PrimarySubjectID (FK, int, null)
        public int? PrimarySubjectID { get; set; }
        //FK FithSubjectID (FK, int, null)
        public int? FithSubjectID { get; set; }
        //FK RupEditorOrgUnitID (FK, int, null)
        public int? RupEditorOrgUnitID { get; set; }
        public bool Deleted { get; set; }
        public string? ShortTitle { get; set; }
        public string? Description { get; set; }
        public int? ESUVOID { get; set; }
        public bool? Classifer { get; set; }
        public int? EducationalProgramStatus { get; set; }
        public int? EducationalProgramType { get; set; }
        public int? Classifer2 { get; set; }
        public string? NoBDID { get; set; }
        public bool ReadyToSendESUVO { get; set; }
        public bool? DoubleDiploma { get; set; }
        public bool? Jointep { get; set; }
        public bool? Is_interdisciplinary { get; set; }
        public bool? Is_not_active { get; set; }
        public decimal? EducationDuration { get; set; }
        public bool? IsPrikladnoy { get; set; }


        //FK_Edu_Specialities_Edu_OrgUnits
        public Edu_OrgUnits? Edu_OrgUnits { get; set; }
        //FK_Edu_Specialities_Edu_SchoolSubjects2
        //FK_Edu_Specialities_Edu_SchoolSubjects3
        public Edu_SchoolSubjects? SchoolSubjects { get; set; }
        //FK_Edu_Specialities_Edu_SpecialityLevels1
        public EduSpecialityLevels? EduSpecialityLevels { get; set; }

    }
}
