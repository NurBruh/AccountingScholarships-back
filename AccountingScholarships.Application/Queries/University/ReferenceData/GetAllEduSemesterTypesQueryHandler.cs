using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduSemesterTypesQueryHandler : IRequestHandler<GetAllEduSemesterTypesQuery, IReadOnlyList<Edu_SemesterTypesDto>>
{
    private readonly ISsoRepository<Edu_SemesterTypes> _repository;

    public GetAllEduSemesterTypesQueryHandler(ISsoRepository<Edu_SemesterTypes> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_SemesterTypesDto>> Handle(GetAllEduSemesterTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_SemesterTypesDto
            {
                ID = e.ID,
                Title = e.Title,
                OrderBy = e.OrderBy
            })
            .ToList()
            .AsReadOnly();
    }
}
