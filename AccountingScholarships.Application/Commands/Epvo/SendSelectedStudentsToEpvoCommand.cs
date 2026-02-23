using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo
{
    public record SendSelectedStudentsToEpvoCommand(IList<string> IINs) : IRequest<int>
    {
    }
}
