using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllSpecializationsQueryHandler
    : IRequestHandler<GetAllSpecializationsQuery, IReadOnlyList<EpvoSpecializationsDto>>
{
    private readonly IEpvoSsoRepository<Specializations> _repository;

    public GetAllSpecializationsQueryHandler(IEpvoSsoRepository<Specializations> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EpvoSpecializationsDto>> Handle(
        GetAllSpecializationsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new EpvoSpecializationsDto
        {
            Created = s.Created,
            Deleted = s.Deleted,
            Id = s.Id,
            UniversityId = s.UniversityId,
            Modified = s.Modified,
            NameEn = s.NameEn,
            NameKz = s.NameKz,
            NameRu = s.NameRu,
            ProfCafId = s.ProfCafId,
            DescriptionEn = s.DescriptionEn,
            DescriptionKz = s.DescriptionKz,
            DescriptionRu = s.DescriptionRu,
            DoubleDiploma = s.DoubleDiploma,
            EduProgType = s.EduProgType,
            IsEducationProgram = s.IsEducationProgram,
            JointEp = s.JointEp,
            PartnerName = s.PartnerName,
            PartnerUniverId = s.PartnerUniverId,
            SpecializationCode = s.SpecializationCode,
            StatusEp = s.StatusEp,
            UniversityType = s.UniversityType,
            IsInterdisciplinary = s.IsInterdisciplinary,
            ProfessionId = s.professionId,
            TypeCode = s.TypeCode,
            IgnoreRms = s.IgnoreRms,
            AcademicDegreeBaAwarded = s.AcademicDegreeBaAwarded,
        }).ToList().AsReadOnly();
    }
}
