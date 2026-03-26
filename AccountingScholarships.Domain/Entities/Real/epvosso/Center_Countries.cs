using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.epvosso
{
    public class Center_Countries
    {
        public string? Alfa2_Code { get; set; }
        public string? Alfa3_Code { get; set; }
        public string? Country_Code { get; set; }
        public string? Full_NameEn { get; set; }
        public string? Full_NameKz { get; set; }
        public string? Full_NameRu { get; set; }
        public int Id { get; set; }
        public int? Id_Regions { get; set; }
        public string? NameEn { get; set; }
        public string? NameKz { get; set; }
        public string? NameRu { get; set; }
        public DateTime? Update_Date { get; set; }
    }
}
