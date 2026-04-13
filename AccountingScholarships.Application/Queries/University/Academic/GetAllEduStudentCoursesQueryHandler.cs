using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public class GetAllEduStudentCoursesQueryHandler : IRequestHandler<GetAllEduStudentCoursesQuery, IReadOnlyList<Edu_StudentCoursesDto>>
{
    private readonly ISsoRepository<Edu_StudentCourses> _repository;

    public GetAllEduStudentCoursesQueryHandler(ISsoRepository<Edu_StudentCourses> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_StudentCoursesDto>> Handle(GetAllEduStudentCoursesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllWithIncludesAsync(new[] { "Student", "SemesterCourse", "Level" }, cancellationToken);
        return entities.Select(e => new Edu_StudentCoursesDto
        {
            ID = e.ID,
            StudentID = e.StudentID,
            SemesterCourseID = e.SemesterCourseID,
            RegisteredBy = e.RegisteredBy,
            RegisteredOn = e.RegisteredOn,
            Grade1 = e.Grade1,
            Grade2 = e.Grade2,
            ExamGrade = e.ExamGrade,
            LetterGrade = e.LetterGrade,
            LastUpdatedBy = e.LastUpdatedBy,
            LastUpdatedOn = e.LastUpdatedOn,
            ExtraGrade = e.ExtraGrade,
            LevelID = e.LevelID,
            CourseAttributeID = e.CourseAttributeID,
            MissingPercentage = e.MissingPercentage,
            MissingFailure = e.MissingFailure,
            Transfer = e.Transfer,
            Ido = e.Ido,
            Student = e.Student == null ? null : new Edu_StudentCoursesDto.StudentRefDto
            {
                StudentID = e.Student.StudentID,
                Year = e.Student.Year
            },
            SemesterCourse = e.SemesterCourse == null ? null : new Edu_StudentCoursesDto.SemesterCourseRefDto
            {
                ID = e.SemesterCourse.ID,
                Code = e.SemesterCourse.Code,
                Title = e.SemesterCourse.Title
            },
            Level = e.Level == null ? null : new Edu_StudentCoursesDto.LevelRefDto
            {
                ID = e.Level.ID,
                Title = e.Level.Title
            }
        }).ToList().AsReadOnly();
    }
}
