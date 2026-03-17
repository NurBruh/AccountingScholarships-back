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
        //UserID (FK, int, not null)
        public int UserID { get; set; }
        //DocumentID (FK, int, not null)
        public int? DocumentID { get; set; }
        //IssuedByID (FK, int, null)
        public int? IssuedByID { get; set; }
        public string? IssuedByText { get; set; }
        public DateOnly? IssuedOn { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public string? FileName { get; set; }
        public byte[]? FileData { get; set; }
        public string? DescriptionText { get; set; }

        //FK_Edu_UserDocuments_Edu_DocumentIssueOrgs
        public Edu_DocumentIssueOrgs? Edu_DocumentIssueOrgs { get; set; }
        //FK_Edu_UserDocuments_Edu_UserDocumentTypes
        public Edu_UserDocumentTypes? Edu_DocumentTypes { get; set; }
        //FK_Edu_UserDocuments_Edu_Users
        public EduUsers? Edu_Users { get; set; }
    }
}
