using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Scollarship_Students_Info
    {
        public int Id { get; set; }
        public string? Iin { get; set; }
        public string? Full_Name { get; set; }
        public string? Iic { get; set; }
        public string? Bic { get; set; }
        public int? studentID { get; set; }
        public DateTime? Updated_Date { get; set; }
    }
}
