using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_Specializations_OrgUnits
    {
        public int? SpecializationID { get; set; }
        public int? OrgUnitID { get; set; }

        // Navigation Properties
        public Edu_Specializations? Specialization { get; set; }
        public Edu_OrgUnits? OrgUnit { get; set; }
    }
}
