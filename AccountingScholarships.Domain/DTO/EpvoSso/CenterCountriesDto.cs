using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.DTO.EpvoSso
{
    public class CenterCountriesDto
    {
        public int Id { get; set; }
        public string? Alfa2_Code { get; set; }
        public string? Alfa3_Code { get; set; }
        public string? CountryCode { get; set; }
        public string? NameRu { get; set; }
        public string? NameKz { get; set; }
        public string? NameEn { get; set; }
    }
}
