using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetAllEduCountriesQueryHandler : IRequestHandler<GetAllEduCountriesQuery, IReadOnlyList<Edu_CountriesDto>>
{
    private readonly ISsoRepository<Edu_Countries> _repository;
    public GetAllEduCountriesQueryHandler(ISsoRepository<Edu_Countries> repository) { _repository = repository; }
    public async Task<IReadOnlyList<Edu_CountriesDto>> Handle(GetAllEduCountriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_CountriesDto { ID = e.ID, Title = e.Title, ESUVOCitizenshipCountryID = e.ESUVOCitizenshipCountryID }).ToList().AsReadOnly();
    }
}
