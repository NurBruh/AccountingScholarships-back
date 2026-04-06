using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.Epvo;

public class GetSsoEpvoComparisonQuery : IRequest<SsoEpvoComparisonDto>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public string Filter { get; set; } = "all"; // all, diff, sso-only, epvo-only
}
