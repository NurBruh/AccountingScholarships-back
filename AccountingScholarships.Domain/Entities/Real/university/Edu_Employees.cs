using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_Employees
    {
        public int ID { get; set; } // PK, FK -> Edu_Users.ID
        public bool IsAdvisor { get; set; }
        public int? RoleGroupId { get; set; }

        // Navigation Properties
        public Edu_Users User { get; set; }
        public ICollection<Edu_EmployeePositions> Positions { get; set; } = new List<Edu_EmployeePositions>();
        public ICollection<Edu_Students> AdvisedStudents { get; set; } = new List<Edu_Students>();
    }
}
