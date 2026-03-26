using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduCourseTypeDvoQueryHandler : IRequestHandler<GetAllEduCourseTypeDvoQuery, IReadOnlyList<Edu_CourseTypeDvoDto>>
{
    private readonly ISsoRepository<Edu_CourseTypeDvo> _repository;

    public GetAllEduCourseTypeDvoQueryHandler(ISsoRepository<Edu_CourseTypeDvo> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_CourseTypeDvoDto>> Handle(GetAllEduCourseTypeDvoQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_CourseTypeDvoDto
            {
                Id = e.Id,
                Title = e.Title
            })
            .ToList()
            .AsReadOnly();
    }
}
