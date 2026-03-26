using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduCourseTypesQueryHandler : IRequestHandler<GetAllEduCourseTypesQuery, IReadOnlyList<Edu_CourseTypesDto>>
{
    private readonly ISsoRepository<Edu_CourseTypes> _repository;

    public GetAllEduCourseTypesQueryHandler(ISsoRepository<Edu_CourseTypes> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_CourseTypesDto>> Handle(GetAllEduCourseTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_CourseTypesDto
            {
                ID = e.ID,
                Title = e.Title,
                Code = e.Code,
                EctsCoefficient = e.EctsCoefficient,
                ShortTitle = e.ShortTitle
            })
            .ToList()
            .AsReadOnly();
    }
}
