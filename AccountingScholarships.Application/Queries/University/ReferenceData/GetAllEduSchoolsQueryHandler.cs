using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduSchoolsQueryHandler : IRequestHandler<GetAllEduSchoolsQuery, IReadOnlyList<Edu_SchoolsDto>>
{
    private readonly ISsoRepository<Edu_Schools> _repository;

    public GetAllEduSchoolsQueryHandler(ISsoRepository<Edu_Schools> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_SchoolsDto>> Handle(GetAllEduSchoolsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_SchoolsDto
            {
                ID = e.ID,
                SchoolTypeID = e.SchoolTypeID,
                SchoolRegionStatusID = e.SchoolRegionStatusID,
                LocalityID = e.LocalityID,
                Number = e.Number,
                Title = e.Title,
                ShortTitle = e.ShortTitle
            })
            .ToList()
            .AsReadOnly();
    }
}
