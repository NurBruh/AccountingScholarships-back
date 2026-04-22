using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduLocalitiesQueryHandler : IRequestHandler<GetAllEduLocalitiesQuery, IReadOnlyList<Edu_LocalitiesDto>>
{
    private readonly ISsoRepository<Edu_Localities> _repository;

    public GetAllEduLocalitiesQueryHandler(ISsoRepository<Edu_Localities> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_LocalitiesDto>> Handle(GetAllEduLocalitiesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        return entities
            .Select(e => new Edu_LocalitiesDto
            {
                ID = e.ID,
                TypeID = e.TypeID,
                Title = e.Title,
                ParentID = e.ParentID,
                ESUVOCenterKatoCode = e.ESUVOCenterKatoCode
            })
            .ToList()
            .AsReadOnly();
    }
}
