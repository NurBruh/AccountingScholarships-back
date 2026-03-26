using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduSchoolRegionStatusesQueryHandler : IRequestHandler<GetAllEduSchoolRegionStatusesQuery, IReadOnlyList<Edu_SchoolRegionStatusesDto>>
{
    private readonly ISsoRepository<Edu_SchoolRegionStatuses> _repository;

    public GetAllEduSchoolRegionStatusesQueryHandler(ISsoRepository<Edu_SchoolRegionStatuses> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_SchoolRegionStatusesDto>> Handle(GetAllEduSchoolRegionStatusesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_SchoolRegionStatusesDto
            {
                ID = e.ID,
                Title = e.Title
            })
            .ToList()
            .AsReadOnly();
    }
}
