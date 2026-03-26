using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_SemesterTypes
    {
        public int ID { get; set; }
        public string Title { get; set; } = null!;
        public int OrderBy { get; set; }
    }
}
