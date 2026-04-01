using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.epvosso
{
    public class Profession_Epvo_New
    {
        public string? TypeCode { get; set; }
        public int? UniversityId { get; set; }
        public int? ProfessionId { get; set; }
        public string? ProfessionNameRu { get; set; }
        public string? ProfessionNameKz { get; set; }
        public string? ProfessionNameEn { get; set; }
        public string? DescriptionRu { get; set; }
        public string? DescriptionKz { get; set; }
        public string? DescriptionEn { get; set; }
        public string? ProfessionCode { get; set; }
        public string? PartnerName { get; set; }
        public string? Created { get; set; }
        public string? DdStart { get; set; }
        public string? Deleted { get; set; }
        public int? AdvisorId { get; set; }
        public int? AccreditAgencyId { get; set; }
        public string? AccreditValidity { get; set; }
        public string? AccreditInstMark { get; set; }
        public string? Code { get; set; }
        public int? TrainingDirectionsId { get; set; }
        public string? Classifier { get; set; }


    }
}
