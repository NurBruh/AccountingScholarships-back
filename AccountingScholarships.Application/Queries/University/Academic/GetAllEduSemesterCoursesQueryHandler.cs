using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public class GetAllEduSemesterCoursesQueryHandler : IRequestHandler<GetAllEduSemesterCoursesQuery, IReadOnlyList<Edu_SemesterCoursesDto>>
{
    private readonly ISsoRepository<Edu_SemesterCourses> _repository;

    public GetAllEduSemesterCoursesQueryHandler(ISsoRepository<Edu_SemesterCourses> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_SemesterCoursesDto>> Handle(GetAllEduSemesterCoursesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllWithIncludesAsync(
            new[] { "Semester", "OrgUnit", "ControlType", "CourseType", "Language", "CourseTypeDvo" },
            cancellationToken);
        return entities.Select(e => new Edu_SemesterCoursesDto
        {
            ID = e.ID,
            SemesterID = e.SemesterID,
            Code = e.Code,
            Title = e.Title,
            OrgUnitID = e.OrgUnitID,
            Credits = e.Credits,
            EctsCredits = e.EctsCredits,
            ControlTypeID = e.ControlTypeID,
            CourseTypeID = e.CourseTypeID,
            CourseDVOTypeID = e.CourseDVOTypeID,
            Lectures = e.Lectures,
            Practices = e.Practices,
            Labs = e.Labs,
            LastUpdatedBy = e.LastUpdatedBy,
            LastUpdatedOn = e.LastUpdatedOn,
            LanguageID = e.LanguageID,
            CourseTypeDvoId = e.CourseTypeDvoId,
            Semester = e.Semester == null ? null : new Edu_SemesterCoursesDto.SemesterRefDto
            {
                ID = e.Semester.ID,
                Title = e.Semester.Title,
                StudyYear = e.Semester.StudyYear
            },
            OrgUnit = e.OrgUnit == null ? null : new Edu_SemesterCoursesDto.OrgUnitRefDto
            {
                ID = e.OrgUnit.ID,
                Title = e.OrgUnit.Title
            },
            ControlType = e.ControlType == null ? null : new Edu_SemesterCoursesDto.SimpleRefDto
            {
                ID = e.ControlType.ID,
                Title = e.ControlType.Title
            },
            CourseType = e.CourseType == null ? null : new Edu_SemesterCoursesDto.SimpleRefDto
            {
                ID = e.CourseType.ID,
                Title = e.CourseType.Title
            },
            Language = e.Language == null ? null : new Edu_SemesterCoursesDto.SimpleRefDto
            {
                ID = e.Language.ID,
                Title = e.Language.Title
            },
            CourseTypeDvo = e.CourseTypeDvo == null ? null : new Edu_SemesterCoursesDto.CourseTypeDvoRefDto
            {
                Id = e.CourseTypeDvo.Id,
                Title = e.CourseTypeDvo.Title
            }
        }).ToList().AsReadOnly();
    }
}
