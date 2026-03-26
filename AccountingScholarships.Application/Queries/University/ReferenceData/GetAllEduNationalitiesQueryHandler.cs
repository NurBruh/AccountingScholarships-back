using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduNationalitiesQueryHandler : IRequestHandler<GetAllEduNationalitiesQuery, IReadOnlyList<Edu_NationalitiesDto>>
{
    private readonly ISsoRepository<Edu_Nationalities> _repository;

    public GetAllEduNationalitiesQueryHandler(ISsoRepository<Edu_Nationalities> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_NationalitiesDto>> Handle(GetAllEduNationalitiesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_NationalitiesDto { ID = e.ID, Title = e.Title }).ToList().AsReadOnly();
    }
}
