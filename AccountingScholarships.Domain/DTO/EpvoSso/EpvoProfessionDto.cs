using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.DTO.EpvoSso
{
    public class EpvoProfessionDto
    {
        public int ProfessionId { get; set; }
        public string? Code { get; set; }
        public string? ProfessionCode { get; set; }
        public string? ProfessionNameRu { get; set; }
        public string? ProfessionNameKz { get; set; }
        public string? ProfessionNameEn { get; set; }
        public bool? DoubleDiploma { get; set; }
        public int? UniversityId { get; set; }
        public string? TypeCode { get; set; }
    }
}
