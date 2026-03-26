using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_Entrants
    {
        public int EntrantID { get; set; } // PK, FK -> Edu_Users.ID
        public DateTime RegisteredOn { get; set; }
        public int? LevelID { get; set; } // FK
        public int StatusID { get; set; } // FK
        public bool? CheckedByAdmissions { get; set; }
        public string? AdmissionsUserID { get; set; }
        public string? SecretaryUserID { get; set; }
        public bool AllowCheckByDoctor { get; set; }
        public bool? CheckedByDoctor { get; set; }
        public string? DoctorUserID { get; set; }
        public bool? CheckedByInstituteHead { get; set; }
        public bool? CheckedByDPOEmployee { get; set; }
        public bool? CheckedByDPOHead { get; set; }
        public bool? CheckedByOR { get; set; }
        public string? ORUserID { get; set; }
        public DateTime? DocumentCheckTime { get; set; }
        public string? FormState { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public bool? HasAppealation { get; set; }
        public string? AppealReason { get; set; }
        public bool? Application { get; set; }
        public bool? HasReceipt { get; set; }
        public int? HearAboutID { get; set; }
        public string? HearAboutText { get; set; }
        public string? Choose { get; set; }
        public DateTime? AddressDocUploadTime { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public int? DisabilityGroupID { get; set; }
        public int? RefLinkId { get; set; }
        public int? PreId { get; set; }
        public int? FilialCityID { get; set; }

        // Navigation Properties
        public Edu_Users User { get; set; }
        public Edu_SpecialityLevels? Level { get; set; }
        public Edu_EntrantStatuses Status { get; set; }
    }
}
