using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduCourseTypeByIdQueryHandler : IRequestHandler<GetEduCourseTypeByIdQuery, Edu_CourseTypesDto?>
{
    private readonly ISsoRepository<Edu_CourseTypes> _repository;
    public GetEduCourseTypeByIdQueryHandler(ISsoRepository<Edu_CourseTypes> repository) { _repository = repository; }
    public async Task<Edu_CourseTypesDto?> Handle(GetEduCourseTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (e is null) return null;
        return new Edu_CourseTypesDto { ID = e.ID, Title = e.Title, Code = e.Code, EctsCoefficient = e.EctsCoefficient, ShortTitle = e.ShortTitle };
    }
}
