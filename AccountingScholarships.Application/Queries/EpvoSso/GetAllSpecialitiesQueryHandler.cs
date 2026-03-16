using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllSpecialitiesQueryHandler
    : IRequestHandler<GetAllSpecialitiesQuery, IReadOnlyList<EpvoSpecialitiesDto>>
{
    private readonly IEpvoSsoRepository<SpecialitiesEpvo> _repository;

    public GetAllSpecialitiesQueryHandler(IEpvoSsoRepository<SpecialitiesEpvo> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EpvoSpecialitiesDto>> Handle(
        GetAllSpecialitiesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new EpvoSpecialitiesDto
        {
            UniversityId = s.UniversityId,
            Id = s.Id,
            ProfCafId = s.profCafId,
            NameRu = s.NameRu,
            NameKz = s.NameKz,
            NameEn = s.NameEn,
            DoubleDiploma = s.DoubleDiploma,
            JointEp = s.JointEp,
            SpecializationCode = s.SpecializationCode,
            StatusEp = s.StatusEp,
            EduProgType = s.EduProgType,
            Default = s.Default,
            Interdisciplinary = s.Interdisciplinary,
            EducationProgram = s.EducationProgram,
        }).ToList().AsReadOnly();
    }
}
