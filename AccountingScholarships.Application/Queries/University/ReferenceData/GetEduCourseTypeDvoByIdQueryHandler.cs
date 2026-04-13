using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduCourseTypeDvoByIdQueryHandler : IRequestHandler<GetEduCourseTypeDvoByIdQuery, Edu_CourseTypeDvoDto?>
{
    private readonly ISsoRepository<Edu_CourseTypeDvo> _repository;
    public GetEduCourseTypeDvoByIdQueryHandler(ISsoRepository<Edu_CourseTypeDvo> repository) { _repository = repository; }
    public async Task<Edu_CourseTypeDvoDto?> Handle(GetEduCourseTypeDvoByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (e is null) return null;
        return new Edu_CourseTypeDvoDto { Id = e.Id, Title = e.Title };
    }
}
