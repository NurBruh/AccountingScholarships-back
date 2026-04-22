using MediatR;
using AccountingScholarships.Application.DTO.EpvoSso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Application.Queries.EpvoSso
{
    public record GetAllEpvoProfession2025Query : IRequest<IReadOnlyList<Profession_2025Dto>>;

}
