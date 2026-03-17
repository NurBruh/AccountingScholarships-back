using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.university
{
    public class Edu_StudentStatuses
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? NOBDID { get; set; }

        // Navigation Properties
        public ICollection<EduStudents> Students { get; set; } = new List<EduStudents>();
    }
}
