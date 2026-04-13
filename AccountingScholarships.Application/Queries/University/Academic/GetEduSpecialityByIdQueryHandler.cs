using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public class GetEduSpecialityByIdQueryHandler : IRequestHandler<GetEduSpecialityByIdQuery, Edu_SpecialitiesDto?>
{
    private readonly ISsoRepository<Edu_Specialities> _repository;

    public GetEduSpecialityByIdQueryHandler(ISsoRepository<Edu_Specialities> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_SpecialitiesDto?> Handle(GetEduSpecialityByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.FindFirstWithIncludesAsync(
            x => x.ID == request.Id,
            new[] { "Level", "PrimarySubject", "FithSubject", "RupEditorOrgUnit" },
            cancellationToken);

        if (e is null) return null;

        return new Edu_SpecialitiesDto
        {
            ID = e.ID,
            Code = e.Code,
            Title = e.Title,
            YearsOfStudy = e.YearsOfStudy,
            Diaspora = e.Diaspora,
            VillageQuota = e.VillageQuota,
            Deleted = e.Deleted,
            ShortTitle = e.ShortTitle,
            Description = e.Description,
            ESUVOID = e.ESUVOID,
            Classifier = e.Classifier,
            EducationalProgramStatus = e.EducationalProgramStatus,
            EducationalProgramType = e.EducationalProgramType,
            Classifier2 = e.Classifier2,
            NoBDID = e.NoBDID,
            ReadyToSendESUVO = e.ReadyToSendESUVO,
            DoubleDiploma = e.DoubleDiploma,
            Jointep = e.Jointep,
            Is_interdisciplinary = e.Is_interdisciplinary,
            is_not_active = e.is_not_active,
            EducationDuration = e.EducationDuration,
            isPrikladnoy = e.isPrikladnoy,
            LevelID = e.LevelID,
            PrimarySubjectID = e.PrimarySubjectID,
            FithSubjectID = e.FithSubjectID,
            RupEditorOrgUnitID = e.RupEditorOrgUnitID,
            Level = e.Level == null ? null : new Edu_SpecialityLevelsDto
            {
                ID = e.Level.ID,
                Title = e.Level.Title,
                NoBDID = e.Level.NoBDID
            },
            PrimarySubject = e.PrimarySubject == null ? null : new Edu_SpecialitiesDto.SchoolSubjectRefDto
            {
                ID = e.PrimarySubject.ID,
                Title = e.PrimarySubject.Title,
                Number = e.PrimarySubject.Number,
                IsRequired = e.PrimarySubject.IsRequired
            },
            FithSubject = e.FithSubject == null ? null : new Edu_SpecialitiesDto.SchoolSubjectRefDto
            {
                ID = e.FithSubject.ID,
                Title = e.FithSubject.Title,
                Number = e.FithSubject.Number,
                IsRequired = e.FithSubject.IsRequired
            },
            RupEditorOrgUnit = e.RupEditorOrgUnit == null ? null : new Edu_SpecialitiesDto.OrgUnitRefDto
            {
                ID = e.RupEditorOrgUnit.ID,
                Title = e.RupEditorOrgUnit.Title
            }
        };
    }
}
