using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_Schools
    {
        public int ID { get; set; }
        public int SchoolTypeID { get; set; }
        public int SchoolRegionStatusID { get; set; }
        public int? LocalityID { get; set; }
        public string? Number { get; set; }
        public string Title { get; set; }
        public string? ShortTitle { get; set; }

        // Navigation Properties
        public Edu_SchoolTypes SchoolType { get; set; }
        public Edu_SchoolRegionStatuses SchoolRegionStatus { get; set; }
        public Edu_Localities? Locality { get; set; }
    }
}
