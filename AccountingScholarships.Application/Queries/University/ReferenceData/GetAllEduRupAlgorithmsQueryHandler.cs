using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduRupAlgorithmsQueryHandler : IRequestHandler<GetAllEduRupAlgorithmsQuery, IReadOnlyList<Edu_RupAlgorithmsDto>>
{
    private readonly ISsoRepository<Edu_RupAlgorithms> _repository;

    public GetAllEduRupAlgorithmsQueryHandler(ISsoRepository<Edu_RupAlgorithms> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_RupAlgorithmsDto>> Handle(GetAllEduRupAlgorithmsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_RupAlgorithmsDto { ID = e.ID, Title = e.Title }).ToList().AsReadOnly();
    }
}
