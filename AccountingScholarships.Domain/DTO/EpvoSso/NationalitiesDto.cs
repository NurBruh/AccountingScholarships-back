using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.DTO.EpvoSso
{
    public class NationalitiesDto
    {
        public int Id { get; set; }
        public int? Center_NationalitiesId { get; set; }
        public string? NameRu { get; set; }
        public string? NameKz { get; set; }
        public string? NameEn { get; set; }
    }
}
