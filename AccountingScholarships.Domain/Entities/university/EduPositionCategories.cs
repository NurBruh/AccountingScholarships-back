using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.university
{
    public class EduPositionCategories
    {
        public int ID { get; set; }
        public string Title { get; set; }

        // Navigation Properties
        public ICollection<EduPositions> Positions { get; set; } = new List<EduPositions>();
    }
}
