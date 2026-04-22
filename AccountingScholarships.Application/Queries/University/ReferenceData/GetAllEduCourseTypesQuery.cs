using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduCourseTypesQuery : IRequest<IReadOnlyList<Edu_CourseTypesDto>>;
