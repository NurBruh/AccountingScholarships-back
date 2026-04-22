using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduCourseTypeDvoQuery : IRequest<IReadOnlyList<Edu_CourseTypeDvoDto>>;
