using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetNationalityByIdQueryHandler
    : IRequestHandler<GetNationalityByIdQuery, NationalitiesDto?>
{
    private readonly IEpvoSsoRepository<Nationalities> _repository;

    public GetNationalityByIdQueryHandler(IEpvoSsoRepository<Nationalities> repository)
    {
        _repository = repository;
    }

    public async Task<NationalitiesDto?> Handle(
        GetNationalityByIdQuery request, CancellationToken cancellationToken)
    {
        var s = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (s is null) return null;

        return new NationalitiesDto
        {
            Id = s.Id,
            Center_NationalitiesId = s.Center_NationalitiesId,
            NameEn = s.NameEn,
            NameKz = s.NameKz,
            NameRu = s.NameRu,
        };
    }
}
