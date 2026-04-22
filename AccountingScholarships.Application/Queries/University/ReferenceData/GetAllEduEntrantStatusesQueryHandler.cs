using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduEntrantStatusesQueryHandler : IRequestHandler<GetAllEduEntrantStatusesQuery, IReadOnlyList<Edu_EntrantStatusesDto>>
{
    private readonly ISsoRepository<Edu_EntrantStatuses> _repository;

    public GetAllEduEntrantStatusesQueryHandler(ISsoRepository<Edu_EntrantStatuses> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_EntrantStatusesDto>> Handle(GetAllEduEntrantStatusesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_EntrantStatusesDto
            {
                ID = e.ID,
                Title = e.Title
            })
            .ToList()
            .AsReadOnly();
    }
}
