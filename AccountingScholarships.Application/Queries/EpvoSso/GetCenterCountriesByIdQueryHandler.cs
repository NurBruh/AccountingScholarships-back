using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetCenterCountriesByIdQueryHandler
    : IRequestHandler<GetCenterCountriesByIdQuery, CenterCountriesDto?>
{
    private readonly IEpvoSsoRepository<Center_Countries> _repository;

    public GetCenterCountriesByIdQueryHandler(IEpvoSsoRepository<Center_Countries> repository)
    {
        _repository = repository;
    }

    public async Task<CenterCountriesDto?> Handle(
        GetCenterCountriesByIdQuery request, CancellationToken cancellationToken)
    {
        var s = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (s is null) return null;

        return new CenterCountriesDto
        {
            Id = s.Id,
            Alfa2_Code = s.Alfa2_Code,
            Alfa3_Code = s.Alfa3_Code,
            CountryCode = s.CountryCode,
            NameRu = s.NameRu,
            NameKz = s.NameKz,
            NameEn = s.NameEn,
            Full_NameEn = s.Full_NameEn,
            Full_NameKz = s.Full_NameKz,
            Full_NameRu = s.Full_NameRu,
            Id_Regions = s.Id_Regions,
            Update_Date = s.Update_Date,
        };
    }
}
