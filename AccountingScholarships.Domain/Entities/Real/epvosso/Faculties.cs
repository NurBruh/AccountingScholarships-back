using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.epvosso
{
    public class Faculties
    {
        public DateTime? Created { get; set; }
        public int? DialUp { get; set; }
        public int? FacultyDean { get; set; }
        public int FacultyId { get; set; }
        public string? FacultyNameEn { get; set; }
        public string? FacultyNameKz { get; set; }
        public string? FacultyNameRu { get; set; }
        public int? UniversityId { get; set; }
        public string? InformationEn { get; set; }
        public string? InformationKz { get; set; }
        public string? InformationRu { get; set; }
        public int? Proper { get; set; }
        public int? Satellite { get; set; }
        public string? TypeCode { get; set; }
    }
}
