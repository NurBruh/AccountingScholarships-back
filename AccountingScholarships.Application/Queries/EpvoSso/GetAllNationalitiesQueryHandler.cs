using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllNationalitiesQueryHandler
    : IRequestHandler<GetAllNationalitiesQuery, IReadOnlyList<NationalitiesDto>>
{
    private readonly IEpvoSsoRepository<Nationalities> _repository;

    public GetAllNationalitiesQueryHandler(IEpvoSsoRepository<Nationalities> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<NationalitiesDto>> Handle(
        GetAllNationalitiesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new NationalitiesDto
        {
            Id = s.Id,
            Center_NationalitiesId = s.Center_NationalitiesId,
            NameRu = s.NameRu,
            NameKz = s.NameKz,
            NameEn = s.NameEn,
        }).ToList().AsReadOnly();
    }
}
