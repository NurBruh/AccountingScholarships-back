using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Application.DTO
{
    public class Edu_GrantTypesNDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? ESUVOGrantTypeId { get; set; }
        public bool? Deleted { get; set; }
    }
}
