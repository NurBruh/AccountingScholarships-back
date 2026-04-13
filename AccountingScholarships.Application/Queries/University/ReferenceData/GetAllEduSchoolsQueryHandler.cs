using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
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
        var entities = await _repository.GetAllWithIncludesAsync(new[] { "SchoolType", "SchoolRegionStatus", "Locality" }, cancellationToken);

        return entities
            .Select(e => new Edu_SchoolsDto
            {
                ID = e.ID,
                SchoolTypeID = e.SchoolTypeID,
                SchoolRegionStatusID = e.SchoolRegionStatusID,
                LocalityID = e.LocalityID,
                Number = e.Number,
                Title = e.Title,
                ShortTitle = e.ShortTitle,
                SchoolType = e.SchoolType == null ? null : new Edu_SchoolsDto.SchoolTypeRefDto { ID = e.SchoolType.ID, Title = e.SchoolType.Title },
                SchoolRegionStatus = e.SchoolRegionStatus == null ? null : new Edu_SchoolsDto.SchoolRegionStatusRefDto { ID = e.SchoolRegionStatus.ID, Title = e.SchoolRegionStatus.Title },
                Locality = e.Locality == null ? null : new Edu_SchoolsDto.LocalityRefDto { ID = e.Locality.ID, Title = e.Locality.Title, ParentID = e.Locality.ParentID, ESUVOCenterKatoCode = e.Locality.ESUVOCenterKatoCode }
            })
            .ToList()
            .AsReadOnly();
    }
}
