using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetEduNationalityByIdQueryHandler : IRequestHandler<GetEduNationalityByIdQuery, Edu_NationalitiesDto?>
{
    private readonly ISsoRepository<Edu_Nationalities> _repository;

    public GetEduNationalityByIdQueryHandler(ISsoRepository<Edu_Nationalities> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_NationalitiesDto?> Handle(GetEduNationalityByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_NationalitiesDto { ID = entity.ID, Title = entity.Title };
    }
}
