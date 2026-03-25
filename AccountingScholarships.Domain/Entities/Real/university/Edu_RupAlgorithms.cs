using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_RupAlgorithms
    {
        public int ID { get; set; }
        public string? Title { get; set; }

        // Navigation Properties
        public ICollection<Edu_Rups> Rups { get; set; } = new List<Edu_Rups>();
    }
}
