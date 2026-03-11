using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.university
{
    public class EduEmployees
    {
        public int ID { get; set; } // PK, FK -> EduUsers.ID
        public bool IsAdvisor { get; set; }
        public int? RoleGroupId { get; set; }

        // Navigation Properties
        public EduUsers User { get; set; }
        public ICollection<EduEmployeePositions> Positions { get; set; } = new List<EduEmployeePositions>();
        public ICollection<EduStudents> AdvisedStudents { get; set; } = new List<EduStudents>();
    }
}
