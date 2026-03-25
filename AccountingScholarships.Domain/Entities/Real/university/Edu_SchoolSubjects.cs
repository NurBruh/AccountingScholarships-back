using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_SchoolSubjects
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public bool? IsRequired { get; set; }

        // Обратные связи — две разные коллекции, т.к. специальность ссылается на предмет дважды
        public ICollection<Edu_Specialities> PrimarySubjectSpecialities { get; set; } = new List<Edu_Specialities>();
        public ICollection<Edu_Specialities> FithSubjectSpecialities { get; set; } = new List<Edu_Specialities>();
    }
}
