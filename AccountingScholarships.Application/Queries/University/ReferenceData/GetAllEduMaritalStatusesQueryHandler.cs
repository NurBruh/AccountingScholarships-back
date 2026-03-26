using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetAllEduMaritalStatusesQueryHandler : IRequestHandler<GetAllEduMaritalStatusesQuery, IReadOnlyList<Edu_MaritalStatusesDto>>
{
    private readonly ISsoRepository<Edu_MaritalStatuses> _repository;
    public GetAllEduMaritalStatusesQueryHandler(ISsoRepository<Edu_MaritalStatuses> repository) { _repository = repository; }
    public async Task<IReadOnlyList<Edu_MaritalStatusesDto>> Handle(GetAllEduMaritalStatusesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_MaritalStatusesDto { ID = e.ID, Title = e.Title }).ToList().AsReadOnly();
    }
}
