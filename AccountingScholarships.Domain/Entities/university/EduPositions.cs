using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.university
{
    public class EduPositions
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public bool Deleted { get; set; }
        public string? Description { get; set; }
        public int Lectures { get; set; }
        public int Practices { get; set; }
        public int Labs { get; set; }

        // FK свойства
        public int? CategoryID { get; set; }

        // Navigation Properties
        public EduPositionCategories? Category { get; set; }
        public ICollection<EduEmployeePositions> EmployeePositions { get; set; } = new List<EduEmployeePositions>();
    }
}
