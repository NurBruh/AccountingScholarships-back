using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.DTO.EpvoSso
{
    public class SpecialitiesEpvo2025Dto
    {
        public int? UniversityId { get; set; }
        public int Id { get; set; }
        public int? ProfCafId { get; set; }
        public string? NameRu { get; set; }
        public string? SpecializationCode { get; set; }
        public int? StatusEp { get; set; }
        public string? EduProgType { get; set; }
        public int? ProfessionId { get; set; }
    }
}
