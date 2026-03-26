using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllCenterNationalitiesQueryHandler
    : IRequestHandler<GetAllCenterNationalitiesQuery, IReadOnlyList<CenterNationalitiesDto>>
{
    private readonly IEpvoSsoRepository<Center_Nationalities> _repository;

    public GetAllCenterNationalitiesQueryHandler(IEpvoSsoRepository<Center_Nationalities> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CenterNationalitiesDto>> Handle(
        GetAllCenterNationalitiesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new CenterNationalitiesDto
        {
            Id = s.Id,
            Nameru = s.nameru,
            Namekz = s.namekz,
            Nameen = s.nameen,
        }).ToList().AsReadOnly();
    }
}
