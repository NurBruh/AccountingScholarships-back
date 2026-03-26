using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduLocalityByIdQueryHandler : IRequestHandler<GetEduLocalityByIdQuery, Edu_LocalitiesDto?>
{
    private readonly ISsoRepository<Edu_Localities> _repository;
    public GetEduLocalityByIdQueryHandler(ISsoRepository<Edu_Localities> repository) { _repository = repository; }
    public async Task<Edu_LocalitiesDto?> Handle(GetEduLocalityByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (e is null) return null;
        return new Edu_LocalitiesDto { ID = e.ID, TypeID = e.TypeID, Title = e.Title, ParentID = e.ParentID, ESUVOCenterKatoCode = e.ESUVOCenterKatoCode };
    }
}
