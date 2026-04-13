using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetCenterKatoByIdQueryHandler
    : IRequestHandler<GetCenterKatoByIdQuery, CenterKatoDto?>
{
    private readonly IEpvoSsoRepository<Center_Kato> _repository;

    public GetCenterKatoByIdQueryHandler(IEpvoSsoRepository<Center_Kato> repository)
    {
        _repository = repository;
    }

    public async Task<CenterKatoDto?> Handle(
        GetCenterKatoByIdQuery request, CancellationToken cancellationToken)
    {
        var s = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (s is null) return null;

        return new CenterKatoDto
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
        };
    }
}
