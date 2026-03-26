using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_Semesters
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime StartsOn { get; set; }
        public DateTime EndsOn { get; set; }
        public int StudyYear { get; set; }
        public int SemesterTypeID { get; set; }

        // Navigation Properties
        public Edu_SemesterTypes SemesterType { get; set; }
    }
}
