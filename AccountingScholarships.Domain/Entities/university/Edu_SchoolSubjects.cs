using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.university
{
    public class Edu_SchoolSubjects
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public bool IsRequired { get; set; }

        //PK_Edu_SchoolSubjects
    }
}
