using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.university
{
    public class EduEducationDurations
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string? ShortTitle { get; set; }
        public string? NoBDIId { get; set; }

        // FK свойства
        public int LevelID { get; set; }

        // Navigation Properties
        public EduSpecialityLevels Level { get; set; }
        public ICollection<EduStudents> Students { get; set; } = new List<EduStudents>();
    }
}
