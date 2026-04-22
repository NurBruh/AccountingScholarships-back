using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduCountryByIdQueryHandler : IRequestHandler<GetEduCountryByIdQuery, Edu_CountriesDto?>
{
    private readonly ISsoRepository<Edu_Countries> _repository;
    public GetEduCountryByIdQueryHandler(ISsoRepository<Edu_Countries> repository) { _repository = repository; }
    public async Task<Edu_CountriesDto?> Handle(GetEduCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_CountriesDto { ID = entity.ID, Title = entity.Title, ESUVOCitizenshipCountryID = entity.ESUVOCitizenshipCountryID };
    }
}
