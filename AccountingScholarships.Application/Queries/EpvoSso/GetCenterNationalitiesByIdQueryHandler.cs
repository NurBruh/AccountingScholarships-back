using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetCenterNationalitiesByIdQueryHandler
    : IRequestHandler<GetCenterNationalitiesByIdQuery, CenterNationalitiesDto?>
{
    private readonly IEpvoSsoRepository<Center_Nationalities> _repository;

    public GetCenterNationalitiesByIdQueryHandler(IEpvoSsoRepository<Center_Nationalities> repository)
    {
        _repository = repository;
    }

    public async Task<CenterNationalitiesDto?> Handle(
        GetCenterNationalitiesByIdQuery request, CancellationToken cancellationToken)
    {
        var s = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (s is null) return null;

        return new CenterNationalitiesDto
        {
            Id = s.Id,
            Nameru = s.nameru,
            Namekz = s.namekz,
            Nameen = s.nameen,
            Update_Date = s.Update_Date,
        };
    }
}
