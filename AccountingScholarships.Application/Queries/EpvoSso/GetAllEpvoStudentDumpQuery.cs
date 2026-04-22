using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingScholarships.Application.DTO.EpvoSso;

namespace AccountingScholarships.Application.Queries.EpvoSso
{
    public record GetAllEpvoStudentDumpQuery : IRequest<IReadOnlyList<EpvoStudentDumpDto>>;
    
}
