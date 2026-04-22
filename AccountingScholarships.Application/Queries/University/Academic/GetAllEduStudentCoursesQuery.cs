using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public record GetAllEduStudentCoursesQuery : IRequest<IReadOnlyList<Edu_StudentCoursesDto>>;
