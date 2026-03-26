using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_Localities
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public string Title { get; set; }
        public int? ParentID { get; set; }
        public string? ESUVOCenterKatoCode { get; set; }

        // Navigation Properties
        public Edu_LocalityTypes Type { get; set; }
        public Edu_Localities? Parent { get; set; }
        public ICollection<Edu_Localities> Children { get; set; } = new List<Edu_Localities>();
    }
}
