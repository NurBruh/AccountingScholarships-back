using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingScholarships.Domain.DTO;


namespace AccountingScholarships.Domain.Interfaces
{
    public interface IEpvoApiClient
    {
        Task SendStudentsAsync(IList<EpvoSendPayloadDto> students, CancellationToken cancellationToken = default);
    }
}
