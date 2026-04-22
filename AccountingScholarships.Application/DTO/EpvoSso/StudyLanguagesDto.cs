using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Application.DTO.EpvoSso
{
    public class StudyLanguagesDto
    {
        public int Id { get; set; }
        public int? Center_StudyLang_Id { get; set; }
        public string? NameRu { get; set; }
        public string? NameKz { get; set; }
        public string? NameEn { get; set; }
    }
}
