using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public class GetEduUserEducationByIdQueryHandler : IRequestHandler<GetEduUserEducationByIdQuery, Edu_UserEducationDto?>
{
    private readonly ISsoRepository<Edu_UserEducation> _repository;

    public GetEduUserEducationByIdQueryHandler(ISsoRepository<Edu_UserEducation> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_UserEducationDto?> Handle(GetEduUserEducationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindFirstWithIncludesAsync(
            x => x.ID == request.Id,
            new[] { "User", "DocumentType", "DocumentSubType", "StudyLanguage", "Speciality" },
            cancellationToken);
        if (entity is null) return null;
        return new Edu_UserEducationDto
        {
            ID = entity.ID,
            UserID = entity.UserID,
            SchoolID = entity.SchoolID,
            SchoolText = entity.SchoolText,
            GraduatedOn = entity.GraduatedOn,
            DocumentTypeID = entity.DocumentTypeID,
            DocumentSubTypeID = entity.DocumentSubTypeID,
            Number = entity.Number,
            Series = entity.Series,
            IssuedOn = entity.IssuedOn,
            GPA = entity.GPA,
            StudyLanguageID = entity.StudyLanguageID,
            ExtraInfo = entity.ExtraInfo,
            SpecialityID = entity.SpecialityID,
            SpecialityText = entity.SpecialityText,
            Qualification = entity.Qualification,
            IsSecondEducation = entity.IsSecondEducation,
            IsRuralQuota = entity.IsRuralQuota,
            User = entity.User == null ? null : new Edu_UserEducationDto.UserRefDto
            {
                ID = entity.User.ID,
                LastName = entity.User.LastName,
                FirstName = entity.User.FirstName
            },
            DocumentType = entity.DocumentType == null ? null : new Edu_UserEducationDto.SimpleRefDto
            {
                ID = entity.DocumentType.ID,
                Title = entity.DocumentType.Title
            },
            DocumentSubType = entity.DocumentSubType == null ? null : new Edu_UserEducationDto.SimpleRefDto
            {
                ID = entity.DocumentSubType.ID,
                Title = entity.DocumentSubType.Title
            },
            StudyLanguage = entity.StudyLanguage == null ? null : new Edu_UserEducationDto.SimpleRefDto
            {
                ID = entity.StudyLanguage.ID,
                Title = entity.StudyLanguage.Title
            },
            Speciality = entity.Speciality == null ? null : new Edu_UserEducationDto.SpecialityRefDto
            {
                ID = entity.Speciality.ID,
                Code = entity.Speciality.Code,
                Title = entity.Speciality.Title
            }
        };
    }
}
