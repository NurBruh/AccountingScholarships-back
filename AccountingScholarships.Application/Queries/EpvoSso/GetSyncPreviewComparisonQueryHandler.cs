using AccountingScholarships.Application.DTO;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetSyncPreviewComparisonQueryHandler
    : IRequestHandler<GetSyncPreviewComparisonQuery, SyncPreviewComparisonPagedDto>
{
    private readonly ISyncPreviewRepository _repository;

    public GetSyncPreviewComparisonQueryHandler(ISyncPreviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<SyncPreviewComparisonPagedDto> Handle(
        GetSyncPreviewComparisonQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetPreviewAsync(
            request.Page,
            request.PageSize,
            request.Filter,
            request.Search,
            cancellationToken);
    }
}
