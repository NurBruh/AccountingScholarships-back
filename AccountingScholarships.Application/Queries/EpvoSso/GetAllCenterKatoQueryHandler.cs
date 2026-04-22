using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllCenterKatoQueryHandler
    : IRequestHandler<GetAllCenterKatoQuery, IReadOnlyList<CenterKatoDto>>
{
    private readonly IEpvoSsoRepository<Center_Kato> _repository;

    public GetAllCenterKatoQueryHandler(IEpvoSsoRepository<Center_Kato> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CenterKatoDto>> Handle(
        GetAllCenterKatoQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new CenterKatoDto
        {
            Id = s.Id,
            Code = s.Code,
            NameRu = s.NameRu,
            NameKz = s.NameKz,
            FullNameRu = s.FullNameRu,
            FullNameKz = s.FullNameKz,
            Deep = s.Deep,
            UpdateDate = s.UpdateDate,
            RegionCode = s.RegionCode,
            Status = s.Status,
            OldCode = s.OldCode,
            UniversityId = s.UniversityId,
        }).ToList().AsReadOnly();
    }
}
