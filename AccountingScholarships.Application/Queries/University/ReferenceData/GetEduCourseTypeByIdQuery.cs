using AccountingScholarships.Domain.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduCourseTypeByIdQuery(int Id) : IRequest<Edu_CourseTypesDto?>;
