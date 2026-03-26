using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_CourseTypes
    {
        public int ID { get; set; }
        public string Title { get; set; } = null!;
        public string? Code { get; set; }
        public double EctsCoefficient { get; set; }
        public string? ShortTitle { get; set; }
    }
}
