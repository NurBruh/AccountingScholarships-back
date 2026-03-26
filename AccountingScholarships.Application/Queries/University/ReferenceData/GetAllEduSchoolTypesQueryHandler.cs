using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduSchoolTypesQueryHandler : IRequestHandler<GetAllEduSchoolTypesQuery, IReadOnlyList<Edu_SchoolTypesDto>>
{
    private readonly ISsoRepository<Edu_SchoolTypes> _repository;

    public GetAllEduSchoolTypesQueryHandler(ISsoRepository<Edu_SchoolTypes> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_SchoolTypesDto>> Handle(GetAllEduSchoolTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_SchoolTypesDto
            {
                ID = e.ID,
                Title = e.Title
            })
            .ToList()
            .AsReadOnly();
    }
}
