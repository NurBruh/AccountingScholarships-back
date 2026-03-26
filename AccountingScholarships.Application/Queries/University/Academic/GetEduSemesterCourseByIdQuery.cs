using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public record GetEduSemesterCourseByIdQuery(int Id) : IRequest<Edu_SemesterCoursesDto?>;
