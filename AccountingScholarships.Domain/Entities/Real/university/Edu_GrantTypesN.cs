using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_GrantTypesN
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? ESUVOGrantTypeId { get; set; }
        public bool? Deleted { get; set; }
    }
}
