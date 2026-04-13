using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public record GetEduStudentCourseByIdQuery(int Id) : IRequest<Edu_StudentCoursesDto?>;
