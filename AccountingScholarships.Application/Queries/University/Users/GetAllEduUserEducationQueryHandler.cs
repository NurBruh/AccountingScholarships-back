using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Users;

public class GetAllEduUserEducationQueryHandler : IRequestHandler<GetAllEduUserEducationQuery, IReadOnlyList<Edu_UserEducationDto>>
{
    private readonly ISsoRepository<Edu_UserEducation> _repository;

    public GetAllEduUserEducationQueryHandler(ISsoRepository<Edu_UserEducation> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_UserEducationDto>> Handle(GetAllEduUserEducationQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllWithIncludesAsync(
            new[] { "User", "DocumentType", "DocumentSubType", "StudyLanguage", "Speciality" },
            cancellationToken);
        return entities.Select(e => new Edu_UserEducationDto
        {
            ID = e.ID,
            UserID = e.UserID,
            SchoolID = e.SchoolID,
            SchoolText = e.SchoolText,
            GraduatedOn = e.GraduatedOn,
            DocumentTypeID = e.DocumentTypeID,
            DocumentSubTypeID = e.DocumentSubTypeID,
            Number = e.Number,
            Series = e.Series,
            IssuedOn = e.IssuedOn,
            GPA = e.GPA,
            StudyLanguageID = e.StudyLanguageID,
            ExtraInfo = e.ExtraInfo,
            SpecialityID = e.SpecialityID,
            SpecialityText = e.SpecialityText,
            Qualification = e.Qualification,
            IsSecondEducation = e.IsSecondEducation,
            IsRuralQuota = e.IsRuralQuota,
            User = e.User == null ? null : new Edu_UserEducationDto.UserRefDto
            {
                ID = e.User.ID,
                LastName = e.User.LastName,
                FirstName = e.User.FirstName
            },
            DocumentType = e.DocumentType == null ? null : new Edu_UserEducationDto.SimpleRefDto
            {
                ID = e.DocumentType.ID,
                Title = e.DocumentType.Title
            },
            DocumentSubType = e.DocumentSubType == null ? null : new Edu_UserEducationDto.SimpleRefDto
            {
                ID = e.DocumentSubType.ID,
                Title = e.DocumentSubType.Title
            },
            StudyLanguage = e.StudyLanguage == null ? null : new Edu_UserEducationDto.SimpleRefDto
            {
                ID = e.StudyLanguage.ID,
                Title = e.StudyLanguage.Title
            },
            Speciality = e.Speciality == null ? null : new Edu_UserEducationDto.SpecialityRefDto
            {
                ID = e.Speciality.ID,
                Code = e.Speciality.Code,
                Title = e.Speciality.Title
            }
        }).ToList().AsReadOnly();
    }
}
