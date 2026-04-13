using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetAllEduMessengersQueryHandler : IRequestHandler<GetAllEduMessengersQuery, IReadOnlyList<Edu_MessengersDto>>
{
    private readonly ISsoRepository<Edu_Messengers> _repository;
    public GetAllEduMessengersQueryHandler(ISsoRepository<Edu_Messengers> repository) { _repository = repository; }
    public async Task<IReadOnlyList<Edu_MessengersDto>> Handle(GetAllEduMessengersQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_MessengersDto { ID = e.ID, Title = e.Title }).ToList().AsReadOnly();
    }
}
