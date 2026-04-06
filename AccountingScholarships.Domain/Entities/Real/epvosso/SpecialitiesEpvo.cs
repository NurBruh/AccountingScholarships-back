using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.epvosso
{
    public class SpecialitiesEpvo
    {
        public float? UniversityId { get; set; }
        public float? Id { get; set; }
        public float? profCafId { get; set; }
        public string? NameRu { get; set; }
        public string? NameKz { get; set; }
        public string? NameEn { get; set; }
        public bool? DoubleDiploma { get; set; }
        public bool? JointEp { get; set; }
        public string? SpecializationCode { get; set; }
        public float? StatusEp { get; set; }
        public float? EduProgType { get; set; }
        public bool? Default { get; set; }
        public bool? Interdisciplinary { get; set; }
        public bool? EducationProgram { get; set; }
    }
}
