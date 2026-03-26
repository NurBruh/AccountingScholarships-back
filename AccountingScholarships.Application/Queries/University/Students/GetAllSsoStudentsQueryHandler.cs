using MediatR;
using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Interfaces;

namespace AccountingScholarships.Application.Queries.University.Students;

public class GetAllSsoStudentsQueryHandler : IRequestHandler<GetAllSsoStudentsQuery, IReadOnlyList<StudentWithUserDto>>
{
    private readonly IEduStudentRepository _repository;

    public GetAllSsoStudentsQueryHandler(IEduStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<StudentWithUserDto>> Handle(GetAllSsoStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _repository.GetAllWithDetailsAsync(cancellationToken);

        return students.Select(s => new StudentWithUserDto
        {
            StudentID = s.StudentID,
            FullName = s.User?.FullName ?? string.Empty,
            Year = s.Year,
            GPA = s.GPA,
            GPA_Y = s.GPA_Y,
            EctsGPA = s.EctsGPA,
            EctsGPA_Y = s.EctsGPA_Y,
            NeedsDorm = s.NeedsDorm,
            AltynBelgi = s.AltynBelgi,
            IsScholarship = s.IsScholarship,
            IsKNB = s.IsKNB,
            EntryDate = s.EntryDate,
            GraduatedOn = s.GraduatedOn,
            AcademicStatusEndsOn = s.AcademicStatusEndsOn,
            AcademicStatusStartsOn = s.AcademicStatusStartsOn,
            ScholarshipOrderNumber = s.ScholarshipOrderNumber,
            ScholarshipOrderDate = s.ScholarshipOrderDate,
            ScholarshipDateStart = s.ScholarshipDateStart,
            ScholarshipDateEnd = s.ScholarshipDateEnd,
            SpecialityID = s.SpecialityID,
            StatusID = s.StatusID,
            CategoryID = s.CategoryID,
            EducationTypeID = s.EducationTypeID,
            EducationPaymentTypeID = s.EducationPaymentTypeID,
            GrantTypeID = s.GrantTypeID,
            EducationDurationID = s.EducationDurationID,
            StudyLanguageID = s.StudyLanguageID,
            RupID = s.RupID,
            AcademicStatusID = s.AcademicStatusID,
            AdvisorID = s.AdvisorID,
            HosterPrivelegeID = s.HosterPrivelegeID,
            MinorSpecialityID = s.MinorSpecialityID,
            EnrollmentTypeId = s.EnrollmentTypeId,
            FundingID = s.FundingID,
            IsPersonalDataComplete = s.IsPersonalDataComplete,
            LastUpdatedBy = s.LastUpdatedBy,
            LastUpdatedOn = s.LastUpdatedOn,
            ScholarshipTypeID = s.ScholarshipTypeID,
            // Navigation properties
            User = s.User == null ? null : new StudentWithUserDto.UserRefDto
            {
                ID = s.User.ID,
                LastName = s.User.LastName,
                FirstName = s.User.FirstName,
                MiddleName = s.User.MiddleName,
                Email = s.User.Email,
                IIN = s.User.IIN,
                DOB = s.User.DOB,
                Male = s.User.Male,
                MobilePhone = s.User.MobilePhone,
                HomePhone = s.User.HomePhone,
                Resident = s.User.Resident,
            },
            EducationType = s.EducationType == null ? null : new StudentWithUserDto.SimpleRefDto { ID = s.EducationType.ID, Title = s.EducationType.Title },
            EducationPaymentType = s.EducationPaymentType == null ? null : new StudentWithUserDto.SimpleRefDto { ID = s.EducationPaymentType.ID, Title = s.EducationPaymentType.Title },
            GrantType = s.GrantType == null ? null : new StudentWithUserDto.SimpleRefDto { ID = s.GrantType.ID, Title = s.GrantType.Title },
            EducationDuration = s.EducationDuration == null ? null : new StudentWithUserDto.SimpleRefDto { ID = s.EducationDuration.ID, Title = s.EducationDuration.Title },
            StudyLanguage = s.StudyLanguage == null ? null : new StudentWithUserDto.SimpleRefDto { ID = s.StudyLanguage.ID, Title = s.StudyLanguage.Title },
            AcademicStatus = s.AcademicStatus == null ? null : new StudentWithUserDto.SimpleRefDto { ID = s.AcademicStatus.ID, Title = s.AcademicStatus.Title },
            Speciality = s.Speciality == null ? null : new StudentWithUserDto.SpecialityRefDto { ID = s.Speciality.ID, Code = s.Speciality.Code, Title = s.Speciality.Title },
            Status = s.Status == null ? null : new StudentWithUserDto.SimpleRefDto { ID = s.Status.ID, Title = s.Status.Title },
            Category = s.Category == null ? null : new StudentWithUserDto.SimpleRefDto { ID = s.Category.ID, Title = s.Category.Title },
        }).ToList().AsReadOnly();
    }
}
