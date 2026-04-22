using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetProfessionByIdQueryHandler
    : IRequestHandler<GetProfessionByIdQuery, EpvoProfessionDto?>
{
    private readonly IEpvoSsoRepository<Profession> _repository;

    public GetProfessionByIdQueryHandler(IEpvoSsoRepository<Profession> repository)
    {
        _repository = repository;
    }

    public async Task<EpvoProfessionDto?> Handle(
        GetProfessionByIdQuery request, CancellationToken cancellationToken)
    {
        var p = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (p is null) return null;

        return new EpvoProfessionDto
        {
            ProfessionId = p.ProfessionId,
            Code = p.Code,
            ProfessionCode = p.ProfessionCode,
            ProfessionNameRu = p.ProfessionNameRu,
            ProfessionNameKz = p.ProfessionNameKz,
            ProfessionNameEn = p.ProfessionNameEn,
            DoubleDiploma = p.DoubleDiploma,
            UniversityId = p.universityId,
            TypeCode = p.TypeCode,
        };
    }
}
