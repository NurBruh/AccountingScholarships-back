using AccountingScholarships.Domain.DTO.EpvoSso;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Application.Queries.EpvoSso
{
    public record GetAllFacultiesQuery : IRequest<IReadOnlyList<FacultyDto>>
    {
    }
}
