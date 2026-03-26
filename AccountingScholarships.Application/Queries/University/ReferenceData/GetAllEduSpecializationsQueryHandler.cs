using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduSpecializationsQueryHandler : IRequestHandler<GetAllEduSpecializationsQuery, IReadOnlyList<Edu_SpecializationsDto>>
{
    private readonly ISsoRepository<Edu_Specializations> _repository;

    public GetAllEduSpecializationsQueryHandler(ISsoRepository<Edu_Specializations> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_SpecializationsDto>> Handle(GetAllEduSpecializationsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_SpecializationsDto
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
            })
            .ToList()
            .AsReadOnly();
    }
}
