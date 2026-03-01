using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.ScholarshipLoss;

public class GetScholarshipLossRecordsQueryHandler
    : IRequestHandler<GetScholarshipLossRecordsQuery, IReadOnlyList<ScholarshipLossRecordDto>>
{
    private readonly IScholarshipLossRepository _repository;

    public GetScholarshipLossRecordsQueryHandler(IScholarshipLossRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ScholarshipLossRecordDto>> Handle(
        GetScholarshipLossRecordsQuery request, CancellationToken cancellationToken)
    {
        var records = await _repository.GetAllAsync(cancellationToken);

        return records.Select(r => new ScholarshipLossRecordDto
        {
            Id = r.Id,
            FirstName = r.FirstName,
            LastName = r.LastName,
            MiddleName = r.MiddleName,
            IIN = r.IIN,
            LostDate = r.LostDate,
            OrderNumber = r.OrderNumber,
            OrderDate = r.OrderDate,
            Reason = r.Reason,
            ScholarshipName = r.ScholarshipName,
            CreatedAt = r.CreatedAt
        }).ToList().AsReadOnly();
    }
}
