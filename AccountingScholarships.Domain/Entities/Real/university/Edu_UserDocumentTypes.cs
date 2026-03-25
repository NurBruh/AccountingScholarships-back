using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class Edu_UserDocumentTypes
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public ICollection<Edu_UserDocuments> Documents { get; set; } = new List<Edu_UserDocuments>();
    }
}
