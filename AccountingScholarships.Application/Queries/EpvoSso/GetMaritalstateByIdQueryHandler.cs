using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetMaritalstateByIdQueryHandler
    : IRequestHandler<GetMaritalstateByIdQuery, MaritalstatesDto?>
{
    private readonly IEpvoSsoRepository<Maritalstates> _repository;

    public GetMaritalstateByIdQueryHandler(IEpvoSsoRepository<Maritalstates> repository)
    {
        _repository = repository;
    }

    public async Task<MaritalstatesDto?> Handle(
        GetMaritalstateByIdQuery request, CancellationToken cancellationToken)
    {
        var s = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (s is null) return null;

        return new MaritalstatesDto
        {
            Id = s.Id,
            NameEn = s.NameEn,
            NameKz = s.NameKz,
            NameRu = s.NameRu,
        };
    }
}
