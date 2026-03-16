using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.epvosso
{
    public class Nationalities
    {
        public int? Center_NationalitiesId { get; set; }
        public int Id { get; set; }
        public int? Id_University { get; set; }
        public string? NameEn { get; set; }
        public string? NameKz { get; set; }
        public string? NameRu { get; set; }
    }
}
