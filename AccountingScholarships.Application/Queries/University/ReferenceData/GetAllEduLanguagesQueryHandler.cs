using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetAllEduLanguagesQueryHandler : IRequestHandler<GetAllEduLanguagesQuery, IReadOnlyList<Edu_LanguagesDto>>
{
    private readonly ISsoRepository<Edu_Languages> _repository;
    public GetAllEduLanguagesQueryHandler(ISsoRepository<Edu_Languages> repository) { _repository = repository; }
    public async Task<IReadOnlyList<Edu_LanguagesDto>> Handle(GetAllEduLanguagesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_LanguagesDto { ID = e.ID, Title = e.Title, NoBDID = e.NoBDID }).ToList().AsReadOnly();
    }
}
