using AccountingScholarships.Domain.Entities;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.ChangeHistory;

public class GetChangeHistoryQueryHandler : IRequestHandler<GetChangeHistoryQuery, IReadOnlyList<ChangeHistoryRecord>>
{
    private readonly IChangeHistoryRepository _repository;

    public GetChangeHistoryQueryHandler(IChangeHistoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ChangeHistoryRecord>> Handle(GetChangeHistoryQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.IIN))
            return await _repository.GetByIINAsync(request.IIN, cancellationToken);

        return await _repository.GetAllAsync(cancellationToken);
    }
}
