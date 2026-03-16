using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.epvosso
{
    public class SpecialitiesEpvo
    {
        public double? UniversityId { get; set; }
        public double? Id { get; set; }
        public double? profCafId { get; set; }
        public string? NameRu { get; set; }
        public string? NameKz { get; set; }
        public string? NameEn { get; set; }
        public bool? DoubleDiploma { get; set; }
        public bool? JointEp { get; set; }
        public string? SpecializationCode { get; set; }
        public double? StatusEp { get; set; }
        public double? EduProgType { get; set; }
        public bool? Default { get; set; }
        public bool? Interdisciplinary { get; set; }
        public bool? EducationProgram { get; set; }
    }
}
