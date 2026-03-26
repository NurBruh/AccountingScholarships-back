using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduSpecializationByIdQueryHandler : IRequestHandler<GetEduSpecializationByIdQuery, Edu_SpecializationsDto?>
{
    private readonly ISsoRepository<Edu_Specializations> _repository;
    public GetEduSpecializationByIdQueryHandler(ISsoRepository<Edu_Specializations> repository) { _repository = repository; }
    public async Task<Edu_SpecializationsDto?> Handle(GetEduSpecializationByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (e is null) return null;
        return new Edu_SpecializationsDto
        {
            Id = e.Id,
            TitleRu = e.TitleRu,
            TitleKz = e.TitleKz,
            TitleEn = e.TitleEn,
            ShortTitleRu = e.ShortTitleRu,
            ShortTitleKz = e.ShortTitleKz,
            ShortTitleEn = e.ShortTitleEn,
            DescriptionRu = e.DescriptionRu,
            DescriptionKz = e.DescriptionKz,
            DescriptionEn = e.DescriptionEn,
            EducationalProgramType = e.EducationalProgramType,
            EducationalProgramStatus = e.EducationalProgramStatus,
            IsEducationalProgram = e.IsEducationalProgram,
            Code = e.Code,
            LevelId = e.LevelId,
            RupEditorOrgUnitId = e.RupEditorOrgUnitId,
            ChairId = e.ChairId,
            Classifier = e.Classifier,
            ESUVOID = e.ESUVOID,
            NoBDID = e.NoBDID
        };
    }
}
