using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_EmployeePositions
    {
        public int ID { get; set; }
        public DateOnly StartedOn { get; set; }
        public DateOnly? EndedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public float? Rate { get; set; }
        public bool? IsMainPosition { get; set; }
        public int? HrOrderId { get; set; }

        // FK свойства
        public int OrgUnitID { get; set; }
        public int PositionID { get; set; }
        public int EmployeeID { get; set; }

        // Navigation Properties
        public Edu_OrgUnits OrgUnit { get; set; }
        public Edu_Positions Position { get; set; }
        public Edu_Employees Employee { get; set; }
    }
}
