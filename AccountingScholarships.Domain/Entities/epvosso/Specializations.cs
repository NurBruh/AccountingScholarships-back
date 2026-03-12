using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.epvosso
{
    public class Specializations
    {
        public DateTime? Created { get; set; }
        public DateTime? Deleted { get; set; }
        public int Id { get; set; }
        public int? UniversityId { get; set; }
        public DateTime? Modified { get; set; }
        public string? NameEn { get; set; }
        public string? NameKz { get; set; }
        public string? NameRu { get; set; }
        public int? ProfCafId { get; set; }
        public string? DescriptionEn { get; set; }
        public string? DescriptionKz { get; set; }
        public string? DescriptionRu { get; set; }
        public bool? DoubleDiploma { get; set; }
        public int? EduProgType { get; set; }
        public bool? IsEducationProgram { get; set; }
        public bool? JointEp { get; set; }
        public string? PartnerName { get; set; }
        public int? PartnerUniverId { get; set; }
        public string? SpecializationCode { get; set; }
        public int? StatusEp { get; set; }
        public int? UniversityType { get; set; }
        public bool? IsInterdisciplinary { get; set; }
        public int? professionId { get; set; }
        public string? TypeCode { get; set; }
        public bool? IgnoreRms { get; set; }
        public bool? AcademicDegreeBaAwarded { get; set; }
    }
}
