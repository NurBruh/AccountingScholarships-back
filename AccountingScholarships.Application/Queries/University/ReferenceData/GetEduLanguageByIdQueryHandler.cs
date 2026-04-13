using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduLanguageByIdQueryHandler : IRequestHandler<GetEduLanguageByIdQuery, Edu_LanguagesDto?>
{
    private readonly ISsoRepository<Edu_Languages> _repository;
    public GetEduLanguageByIdQueryHandler(ISsoRepository<Edu_Languages> repository) { _repository = repository; }
    public async Task<Edu_LanguagesDto?> Handle(GetEduLanguageByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_LanguagesDto { ID = entity.ID, Title = entity.Title, NoBDID = entity.NoBDID };
    }
}
