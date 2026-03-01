using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.ReferenceData;

public class GetAllReferenceDataQueryHandler : IRequestHandler<GetAllReferenceDataQuery, ReferenceDataDto>
{
    private readonly IReferenceDataRepository _repository;

    public GetAllReferenceDataQueryHandler(IReferenceDataRepository repository)
    {
        _repository = repository;
    }

    public async Task<ReferenceDataDto> Handle(GetAllReferenceDataQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllReferenceDataAsync(cancellationToken);
    }
}
