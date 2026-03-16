using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.epvosso
{
    public class Studycalendar
    {
        public int StudyCalendarId { get; set; }
        public int? UniversityId { get; set; }
        public string? Name { get; set; }
        public int? StudyFormId { get; set; }
        public int? Year { get; set; }
        public int? CalendarTypeId { get; set; }
        public int? ProfessionId { get; set; }
        public int? SpecializationId { get; set; }
        public int? Status { get; set; }
        public int? EntranceYear { get; set; }
        public string? TypeCode { get; set; }
    }
}
