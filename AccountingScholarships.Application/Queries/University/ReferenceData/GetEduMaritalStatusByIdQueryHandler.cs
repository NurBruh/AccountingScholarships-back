using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduMaritalStatusByIdQueryHandler : IRequestHandler<GetEduMaritalStatusByIdQuery, Edu_MaritalStatusesDto?>
{
    private readonly ISsoRepository<Edu_MaritalStatuses> _repository;
    public GetEduMaritalStatusByIdQueryHandler(ISsoRepository<Edu_MaritalStatuses> repository) { _repository = repository; }
    public async Task<Edu_MaritalStatusesDto?> Handle(GetEduMaritalStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_MaritalStatusesDto { ID = entity.ID, Title = entity.Title };
    }
}
