using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.DTO
{
    public class EpvoSendPayloadDto
    {
        public string IIN { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string? Faculty { get; set; }
        public string? Speciality { get; set; }
        public int Course { get; set; }
        public string? GrantName { get; set; }
        public decimal GrantAmount { get; set;  }
        public string? ScholarshipName { get; set; }
        public decimal? ScholarshipAmount { get; set; }
        public string iban { get; set; } = string.Empty;
        public bool isActive { get; set; }  
    }
}
