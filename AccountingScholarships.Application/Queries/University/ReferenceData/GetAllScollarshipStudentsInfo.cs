using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingScholarships.Application.DTO.University;

namespace AccountingScholarships.Application.Queries.University.ReferenceData
{
    public record GetAllScollarshipStudentsInfo : IRequest<IReadOnlyList<Scollarship_Students_InfoDto>>;

}
