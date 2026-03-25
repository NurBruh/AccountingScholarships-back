using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllProfessionsQueryHandler
    : IRequestHandler<GetAllProfessionsQuery, IReadOnlyList<EpvoProfessionDto>>
{
    private readonly IEpvoSsoRepository<Profession> _repository;

    public GetAllProfessionsQueryHandler(IEpvoSsoRepository<Profession> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EpvoProfessionDto>> Handle(
        GetAllProfessionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(p => new EpvoProfessionDto
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
        }).ToList().AsReadOnly();
    }
}
