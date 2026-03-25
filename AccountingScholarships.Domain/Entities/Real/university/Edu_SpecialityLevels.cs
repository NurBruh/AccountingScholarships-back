using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_SpecialityLevels
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string? NoBDID { get; set; }

        // Navigation Properties
        public ICollection<Edu_EducationDurations> EducationDurations { get; set; } = new List<Edu_EducationDurations>();
        public ICollection<Edu_Specialities> Specialities { get; set; } = new List<Edu_Specialities>();
    }
}
