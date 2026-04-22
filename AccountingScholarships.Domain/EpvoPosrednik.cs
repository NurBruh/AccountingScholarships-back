using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain
{
    public class EpvoPosrednik
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string IIN { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Faculty { get; set; }
        public string? Speciality { get; set; }
        public int Course { get; set; }
        public string? GrantName { get; set; }
        public decimal? GrantAmount { get; set; }
        public string? ScholarshipName { get; set; }
        public decimal? ScholarshipAmount { get; set; }
        public DateTime? ScholarshipLostDate { get; set; }
        public DateTime? ScholarshipOrderLostDate { get; set; }
        public DateTime? ScholarshipOrderCandidateDate { get; set; }
        public string? ScholarshipNotes { get; set; }
        public string iban { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime SyncDate { get; set; } = DateTime.UtcNow;
    }
}

