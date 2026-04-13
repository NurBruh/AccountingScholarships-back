using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public class GetEduStudentCourseByIdQueryHandler : IRequestHandler<GetEduStudentCourseByIdQuery, Edu_StudentCoursesDto?>
{
    private readonly ISsoRepository<Edu_StudentCourses> _repository;

    public GetEduStudentCourseByIdQueryHandler(ISsoRepository<Edu_StudentCourses> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_StudentCoursesDto?> Handle(GetEduStudentCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindFirstWithIncludesAsync(
            x => x.ID == request.Id,
            new[] { "Student", "SemesterCourse", "Level" },
            cancellationToken);
        if (entity is null) return null;
        return new Edu_StudentCoursesDto
        {
            ID = entity.ID,
            StudentID = entity.StudentID,
            SemesterCourseID = entity.SemesterCourseID,
            RegisteredBy = entity.RegisteredBy,
            RegisteredOn = entity.RegisteredOn,
            Grade1 = entity.Grade1,
            Grade2 = entity.Grade2,
            ExamGrade = entity.ExamGrade,
            LetterGrade = entity.LetterGrade,
            LastUpdatedBy = entity.LastUpdatedBy,
            LastUpdatedOn = entity.LastUpdatedOn,
            ExtraGrade = entity.ExtraGrade,
            LevelID = entity.LevelID,
            CourseAttributeID = entity.CourseAttributeID,
            MissingPercentage = entity.MissingPercentage,
            MissingFailure = entity.MissingFailure,
            Transfer = entity.Transfer,
            Ido = entity.Ido,
            Student = entity.Student == null ? null : new Edu_StudentCoursesDto.StudentRefDto
            {
                StudentID = entity.Student.StudentID,
                Year = entity.Student.Year
            },
            SemesterCourse = entity.SemesterCourse == null ? null : new Edu_StudentCoursesDto.SemesterCourseRefDto
            {
                ID = entity.SemesterCourse.ID,
                Code = entity.SemesterCourse.Code,
                Title = entity.SemesterCourse.Title
            },
            Level = entity.Level == null ? null : new Edu_StudentCoursesDto.LevelRefDto
            {
                ID = entity.Level.ID,
                Title = entity.Level.Title
            }
        };
    }
}
