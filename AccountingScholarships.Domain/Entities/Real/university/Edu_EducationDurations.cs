using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_EducationDurations
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string? ShortTitle { get; set; }
        public string? NoBDIId { get; set; }

        // FK свойства
        public int LevelID { get; set; }

        // Navigation Properties
        public Edu_SpecialityLevels Level { get; set; }
        public ICollection<Edu_Students> Students { get; set; } = new List<Edu_Students>();
    }
}
