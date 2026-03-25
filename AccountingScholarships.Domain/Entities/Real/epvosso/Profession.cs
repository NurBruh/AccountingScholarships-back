using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.epvosso
{
    public class Profession
    {
        public string? Code { get; set; }
        public DateTime? Created { get; set; }
        public int? Deleted { get; set; }
        public string? DescriptionEn { get; set; }
        public string? DescriptionKz { get; set; }
        public string? DescriptionRu { get; set; }
        public bool? DoubleDiploma { get; set; }
        public int? universityId { get; set; }
        public string? PartnerName { get; set; }
        public string? ProfessionCode { get; set; }
        public int ProfessionId { get; set; }
        public string? ProfessionNameEn { get; set; }
        public string? ProfessionNameKz { get; set; }
        public string? ProfessionNameRu { get; set; }
        public string? Qualificationen { get; set; }
        public string? Qualificationkz { get; set; }
        public string? Qualificationru { get; set; }
        public int? Classifier { get; set; }
        public int? TrainingDirectionsId { get; set; }
        public DateOnly? DdStart { get; set; }
        public int? AdvisorId { get; set; }
        public int? AccreditAgencyId { get; set; }
        public DateTime? AccreditValidity { get; set; }
        public string? AccreditInstMark { get; set; }
        public string? TypeCode { get; set; }

    }
}
