using System.Collections.Generic;
using AccountingScholarships.Application.DTO;

namespace AccountingScholarships.API.Controllers.Real
{
    public class SaveTempBatchRequest
    {
        public List<StudentComparisonDto> Students { get; set; } = new();
    }
    
    public class SyncSessionRequest
    {
        public string SyncSessionId { get; set; } = string.Empty;
    }
}
