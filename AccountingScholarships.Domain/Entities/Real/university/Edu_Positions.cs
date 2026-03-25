using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_Positions
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
        public Edu_PositionCategories? Category { get; set; }
        public ICollection<Edu_EmployeePositions> EmployeePositions { get; set; } = new List<Edu_EmployeePositions>();
    }
}
