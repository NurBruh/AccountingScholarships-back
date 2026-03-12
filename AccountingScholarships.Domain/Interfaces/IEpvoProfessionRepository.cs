using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.epvosso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingScholarships.Domain.Interfaces
{
    public interface IEpvoProfessionRepository : IEpvoSsoRepository<Profession>
    {
        Task<EpvoProfessionDto?> GetAsDtoAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<EpvoProfessionDto>> GetAllAsDtoAsync(CancellationToken ct = default);
    }
}
