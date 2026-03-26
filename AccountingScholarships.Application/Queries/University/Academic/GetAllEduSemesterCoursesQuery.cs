using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public record GetAllEduSemesterCoursesQuery : IRequest<IReadOnlyList<Edu_SemesterCoursesDto>>;
