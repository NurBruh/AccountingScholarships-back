using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllSpecialitiesEpvo2025QueryHandler
    : IRequestHandler<GetAllSpecialitiesEpvo2025Query, IReadOnlyList<SpecialitiesEpvo2025Dto>>
{
    private readonly IEpvoSsoRepository<Specialities_Epvo_2025> _repository;

    public GetAllSpecialitiesEpvo2025QueryHandler(IEpvoSsoRepository<Specialities_Epvo_2025> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<SpecialitiesEpvo2025Dto>> Handle(
        GetAllSpecialitiesEpvo2025Query request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new SpecialitiesEpvo2025Dto
        {
            Id = int.TryParse(s.Id, out var id) ? id : 0,
            UniversityId = int.TryParse(s.UniversityId, out var univId) ? univId : null,
            ProfCafId = int.TryParse(s.ProfCafId, out var profCafId) ? profCafId : null,
            NameRu = s.NameRu,
            SpecializationCode = s.SpecializationCode,
            StatusEp = int.TryParse(s.StatusEp, out var statusEp) ? statusEp : null,
            EduProgType = s.EduProgType,
            ProfessionId = int.TryParse(s.ProfessionId, out var profId) ? profId : null,
        }).ToList().AsReadOnly();
    }
}
