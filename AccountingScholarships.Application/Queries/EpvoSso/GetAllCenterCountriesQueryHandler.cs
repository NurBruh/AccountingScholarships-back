using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllCenterCountriesQueryHandler
    : IRequestHandler<GetAllCenterCountriesQuery, IReadOnlyList<CenterCountriesDto>>
{
    private readonly IEpvoSsoRepository<Center_Countries> _repository;

    public GetAllCenterCountriesQueryHandler(IEpvoSsoRepository<Center_Countries> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CenterCountriesDto>> Handle(
        GetAllCenterCountriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new CenterCountriesDto
        {
            Id = s.Id,
            Alfa2_Code = s.Alfa2_Code,
            Alfa3_Code = s.Alfa3_Code,
            CountryCode = s.CountryCode,
            NameRu = s.NameRu,
            NameKz = s.NameKz,
            NameEn = s.NameEn,
        }).ToList().AsReadOnly();
    }
}
