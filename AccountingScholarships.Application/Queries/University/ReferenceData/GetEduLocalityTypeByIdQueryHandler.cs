using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduLocalityTypeByIdQueryHandler : IRequestHandler<GetEduLocalityTypeByIdQuery, Edu_LocalityTypesDto?>
{
    private readonly ISsoRepository<Edu_LocalityTypes> _repository;
    public GetEduLocalityTypeByIdQueryHandler(ISsoRepository<Edu_LocalityTypes> repository) { _repository = repository; }
    public async Task<Edu_LocalityTypesDto?> Handle(GetEduLocalityTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (e is null) return null;
        return new Edu_LocalityTypesDto { ID = e.ID, Title = e.Title };
    }
}
