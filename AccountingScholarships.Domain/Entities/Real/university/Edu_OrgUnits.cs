using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_OrgUnits
    {
        public int ID { get; set; }
        public int? ParentID { get; set; }
        public string Title { get; set; }
        public bool Deleted { get; set; }
        public string? ShortTitle { get; set; }

        // FK свойства
        public int TypeID { get; set; }

        // Navigation Properties
        public Edu_OrgUnitTypes Type { get; set; }
        public Edu_OrgUnits? Parent { get; set; }
        public ICollection<Edu_OrgUnits> Children { get; set; } = new List<Edu_OrgUnits>();
        public ICollection<Edu_EmployeePositions> EmployeePositions { get; set; } = new List<Edu_EmployeePositions>();
        public ICollection<Edu_Specialities> Edu_Specialities { get; set; } = new List<Edu_Specialities>();
    }
}
