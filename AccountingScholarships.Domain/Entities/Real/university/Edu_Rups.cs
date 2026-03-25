using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_Rups
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
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int? AcademicDegreeId { get; set; }
        public string? RupTitle { get; set; }
        public bool? IncludeToRegOp { get; set; }
        public bool? EducationalProgram { get; set; }
        public int? EducationalProgramId { get; set; }
        public bool? DualProgram { get; set; }
        public int? Hero_WEP_ID { get; set; }
        public string? RupPrivateSignerRU { get; set; }
        public string? RupPrivateSignerEN { get; set; }
        public string? RupPrivateSignerKZ { get; set; }
        public DateTime? AcademCouncilDate { get; set; }
        public string? AcademCouncilNum { get; set; }
        public DateTime? EducCouncilDate { get; set; }
        public string? EducCouncilNum { get; set; }
        public DateTime? AcademCouncilInstDate { get; set; }
        public string? AcademCouncilInstNum { get; set; }
        public int? EducationDirectionId { get; set; }

        // Navigation Properties
        // Navigation Properties
        public Edu_RupAlgorithms? Algorithm { get; set; }
        public Edu_EducationDurations? EducationDuration { get; set; }
        public Edu_Specialities? Speciality { get; set; }

        // Обратная связь
        public ICollection<Edu_Students> Students { get; set; } = new List<Edu_Students>();
    }
}
