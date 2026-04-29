using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.epvosso
{
    public class Scholarship_new
    {
        public int UniversityId { get; set; }
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ScholarshipYear { get; set; }
        public int ScholarshipMonth { get; set; }
        public DateOnly? ScholarshipPayDate { get; set; }
        public float? ScholarshipMoney { get; set; }
        public int? ScholarshipTypeId { get; set; }
        public DateOnly? TerminationDate { get; set; }
        public bool? AdditionalPayment { get; set; }
        public int SectionId { get; set; }
        public int ScholarshipAwardYear { get; set; }
        public int ScholarshipAwardTerm { get; set; }
        public int OverallPerformance { get; set; }
        public string? TypeCode { get; set; }
    }
}
