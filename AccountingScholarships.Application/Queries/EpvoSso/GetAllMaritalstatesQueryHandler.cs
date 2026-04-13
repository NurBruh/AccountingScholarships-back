using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllMaritalstatesQueryHandler
    : IRequestHandler<GetAllMaritalstatesQuery, IReadOnlyList<MaritalstatesDto>>
{
    private readonly IEpvoSsoRepository<Maritalstates> _repository;

    public GetAllMaritalstatesQueryHandler(IEpvoSsoRepository<Maritalstates> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<MaritalstatesDto>> Handle(
        GetAllMaritalstatesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new MaritalstatesDto
        {
            Id = s.Id,
            NameRu = s.NameRu,
            NameKz = s.NameKz,
            NameEn = s.NameEn,
        }).ToList().AsReadOnly();
    }
}
