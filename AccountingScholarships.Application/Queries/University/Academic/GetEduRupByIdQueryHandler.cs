using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public class GetEduRupByIdQueryHandler : IRequestHandler<GetEduRupByIdQuery, Edu_RupsDto?>
{
    private readonly ISsoRepository<Edu_Rups> _repository;

    public GetEduRupByIdQueryHandler(ISsoRepository<Edu_Rups> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_RupsDto?> Handle(GetEduRupByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.FindFirstWithIncludesAsync(
            x => x.ID == request.Id,
            new[] { "Algorithm", "EducationDuration", "Speciality" },
            cancellationToken);

        if (e is null) return null;

        return new Edu_RupsDto
        {
            ID = e.ID,
            SpecialityID = e.SpecialityID,
            SpecialisationID = e.SpecialisationID,
            Year = e.Year,
            SemesterCount = e.SemesterCount,
            AlgorithmID = e.AlgorithmID,
            CreditsLimitId = e.CreditsLimitId,
            IsModular = e.IsModular,
            ApprovedByChair = e.ApprovedByChair,
            ApprovedByChairUserID = e.ApprovedByChairUserID,
            ApprovedByChairOn = e.ApprovedByChairOn,
            ApprovedByOR = e.ApprovedByOR,
            ApprovedByORUserID = e.ApprovedByORUserID,
            ApprovedByOROn = e.ApprovedByOROn,
            Locked = e.Locked,
            EducationDurationID = e.EducationDurationID,
            RejectionReason = e.RejectionReason,
            LastUpdatedBy = e.LastUpdatedBy,
            LastUpdatedOn = e.LastUpdatedOn,
            AcademicDegreeId = e.AcademicDegreeId,
            RupTitle = e.RupTitle,
            IncludeToRegOp = e.IncludeToRegOp,
            EducationalProgram = e.EducationalProgram,
            EducationalProgramId = e.EducationalProgramId,
            DualProgram = e.DualProgram,
            Algorithm = e.Algorithm == null ? null : new Edu_RupsDto.AlgorithmRefDto
            {
                ID = e.Algorithm.ID,
                Title = e.Algorithm.Title
            },
            EducationDuration = e.EducationDuration == null ? null : new Edu_RupsDto.EducationDurationRefDto
            {
                ID = e.EducationDuration.ID,
                Title = e.EducationDuration.Title
            },
            Speciality = e.Speciality == null ? null : new Edu_RupsDto.SpecialityRefDto
            {
                ID = e.Speciality.ID,
                Code = e.Speciality.Code,
                Title = e.Speciality.Title
            }
        };
    }
}
