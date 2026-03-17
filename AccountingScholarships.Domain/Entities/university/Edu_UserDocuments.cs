using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.university
{
    public class Edu_UserDocuments
    {
        public int ID { get; set; }

        // FK свойства
        public int UserID { get; set; }
        public int DocumentTypeID { get; set; }
        public int? IssuedByID { get; set; }

        // Обычные свойства
        public string? IssuedByText { get; set; }
        public DateOnly? IssuedOn { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public string? FileName { get; set; }
        public byte[]? FileData { get; set; }
        public string? DescriptionText { get; set; }

        // Navigation Properties
        public EduUsers User { get; set; }
        public Edu_UserDocumentTypes? DocumentType { get; set; }
        public Edu_DocumentIssueOrgs? IssuedByOrg { get; set; }
    }
}
