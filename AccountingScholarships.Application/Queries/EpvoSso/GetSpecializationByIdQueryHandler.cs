using AccountingScholarships.Domain.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.epvosso;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetSpecializationByIdQueryHandler
    : IRequestHandler<GetSpecializationByIdQuery, EpvoSpecializationsDto?>
{
    private readonly IEpvoSsoRepository<Specializations> _repository;

    public GetSpecializationByIdQueryHandler(IEpvoSsoRepository<Specializations> repository)
    {
        _repository = repository;
    }

    public async Task<EpvoSpecializationsDto?> Handle(
        GetSpecializationByIdQuery request, CancellationToken cancellationToken)
    {
        var s = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (s is null) return null;

        return new EpvoSpecializationsDto
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
        };
    }
}
