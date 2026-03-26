using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_UserAddresses
    {
        public int ID { get; set; } // PK
        public int UserID { get; set; } // FK
        public int AddressTypeID { get; set; } // FK
        public int CountryID { get; set; } // FK
        public int LocalityID { get; set; } // FK
        public string? LocalityText { get; set; }
        public string? AddressText { get; set; }
        public string? Region { get; set; }
        public string? Area { get; set; }
        public string? AddressTextEN { get; set; }

        // Navigation Properties
        public Edu_Users User { get; set; }
        public Edu_AddressTypes AddressType { get; set; }
        public Edu_Countries Country { get; set; }
        public Edu_Localities Locality { get; set; }
    }
}
