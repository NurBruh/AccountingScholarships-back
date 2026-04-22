using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllSpecialitiesNewQueryHandler
    : IRequestHandler<GetAllSpecialitiesNewQuery, IReadOnlyList<EpvoSpecialitiesNewDto>>
{
    private readonly IEpvoSsoRepository<SpecialitiesEpvoNew> _repository;

    public GetAllSpecialitiesNewQueryHandler(IEpvoSsoRepository<SpecialitiesEpvoNew> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EpvoSpecialitiesNewDto>> Handle(
        GetAllSpecialitiesNewQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new EpvoSpecialitiesNewDto
        {
            UniversityId = s.UniversityId,
            Id = s.Id,
            ProfCafId = s.ProfCafId,
            NameRu = s.NameRu,
            DoubleDiploma = s.DoubleDiploma,
            JointEp = s.JointEp,
            UniversityType = s.UniversityType,
            PartnerUniverId = s.PartnerUniverId,
            SpecializationCode = s.SpecializationCode,
            StatusEp = s.StatusEp,
            EduProgType = s.EduProgType,
            IsEducationProgram = s.IsEducationProgram,
            ProfessionId = s.ProfessionId,
            IsInterdisciplinary = s.IsInterdisciplinary,
        }).ToList().AsReadOnly();
    }
}
