using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.epvosso
{
    public class Center_Kato
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? NameRu { get; set; }
        public string? NameKz { get; set; }
        public string? FullNameRu { get; set; }
        public string? FullNameKz { get; set; }
        public int? Deep { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? RegionCode { get; set; }
        public int? Status { get; set; }
        public string? OldCode { get; set; }
        public int? UniversityId { get; set; }
    }
}
