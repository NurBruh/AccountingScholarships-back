using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.university
{
    public class EduMessengers
    {
        public int ID { get; set; }
        public string Title { get; set; }

        // Navigation Properties
        public ICollection<EduUsers> Users { get; set; } = new List<EduUsers>();
    }
}
