using AccountingScholarships.Application.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetSyncPreviewComparisonQuery : IRequest<SyncPreviewComparisonPagedDto>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public string Filter { get; set; } = "all";
    public string? Search { get; set; }
}
