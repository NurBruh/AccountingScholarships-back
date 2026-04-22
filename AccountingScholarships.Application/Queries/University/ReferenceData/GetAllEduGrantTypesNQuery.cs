using MediatR;
using AccountingScholarships.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Application.Queries.University.ReferenceData
{
    public record GetAllEduGrantTypesNQuery : IRequest<IReadOnlyList<Edu_GrantTypesNDto>>;
    
}
