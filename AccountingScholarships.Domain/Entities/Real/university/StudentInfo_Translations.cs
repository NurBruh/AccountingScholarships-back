using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Entities.Real.university
{
    public class StudentInfo_Translations
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public int ObjectID { get; set; }
        public string Language { get; set; }
        public string? Value { get; set; }
    }
}
