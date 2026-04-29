using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AccountingScholarships.Domain.Entities.Real.epvosso;

namespace AccountingScholarships.Infrastructure.Services.StudentSync;

public interface ISsoToEpvoMapperService
{
    Task<List<Student_Temp>> MapStudentsAsync(List<string> iinPlts, CancellationToken ct = default);

    /// <summary>
    /// Маппит ВСЕХ активных студентов из SSO в формат ЕПВО.
    /// Аналог выполнения [dbo].[Reload_STUDENT] без фильтра по IIN.
    /// </summary>
    Task<List<Student_Temp>> MapAllAsync(CancellationToken ct = default);
}
