using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public class GetEduSemesterCourseByIdQueryHandler : IRequestHandler<GetEduSemesterCourseByIdQuery, Edu_SemesterCoursesDto?>
{
    private readonly ISsoRepository<Edu_SemesterCourses> _repository;

    public GetEduSemesterCourseByIdQueryHandler(ISsoRepository<Edu_SemesterCourses> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_SemesterCoursesDto?> Handle(GetEduSemesterCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindFirstWithIncludesAsync(
            x => x.ID == request.Id,
            new[] { "Semester", "OrgUnit", "ControlType", "CourseType", "Language", "CourseTypeDvo" },
            cancellationToken);
        if (entity is null) return null;
        return new Edu_SemesterCoursesDto
        {
            ID = entity.ID,
            SemesterID = entity.SemesterID,
            Code = entity.Code,
            Title = entity.Title,
            OrgUnitID = entity.OrgUnitID,
            Credits = entity.Credits,
            EctsCredits = entity.EctsCredits,
            ControlTypeID = entity.ControlTypeID,
            CourseTypeID = entity.CourseTypeID,
            CourseDVOTypeID = entity.CourseDVOTypeID,
            Lectures = entity.Lectures,
            Practices = entity.Practices,
            Labs = entity.Labs,
            LastUpdatedBy = entity.LastUpdatedBy,
            LastUpdatedOn = entity.LastUpdatedOn,
            LanguageID = entity.LanguageID,
            CourseTypeDvoId = entity.CourseTypeDvoId,
            Semester = entity.Semester == null ? null : new Edu_SemesterCoursesDto.SemesterRefDto
            {
                ID = entity.Semester.ID,
                Title = entity.Semester.Title,
                StudyYear = entity.Semester.StudyYear
            },
            OrgUnit = entity.OrgUnit == null ? null : new Edu_SemesterCoursesDto.OrgUnitRefDto
            {
                ID = entity.OrgUnit.ID,
                Title = entity.OrgUnit.Title
            },
            ControlType = entity.ControlType == null ? null : new Edu_SemesterCoursesDto.SimpleRefDto
            {
                ID = entity.ControlType.ID,
                Title = entity.ControlType.Title
            },
            CourseType = entity.CourseType == null ? null : new Edu_SemesterCoursesDto.SimpleRefDto
            {
                ID = entity.CourseType.ID,
                Title = entity.CourseType.Title
            },
            Language = entity.Language == null ? null : new Edu_SemesterCoursesDto.SimpleRefDto
            {
                ID = entity.Language.ID,
                Title = entity.Language.Title
            },
            CourseTypeDvo = entity.CourseTypeDvo == null ? null : new Edu_SemesterCoursesDto.CourseTypeDvoRefDto
            {
                Id = entity.CourseTypeDvo.Id,
                Title = entity.CourseTypeDvo.Title
            }
        };
    }
}
