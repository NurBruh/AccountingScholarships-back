using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.university
{
    public class Edu_OrgUnitTypes
    {
        public int ID { get; set; }
        public string? Title { get; set; }

        // Navigation Properties
        public ICollection<Edu_OrgUnits> OrgUnits { get; set; } = new List<Edu_OrgUnits>();
    }
}
