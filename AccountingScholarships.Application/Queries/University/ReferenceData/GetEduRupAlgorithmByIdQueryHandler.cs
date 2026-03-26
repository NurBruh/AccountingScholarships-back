using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetEduRupAlgorithmByIdQueryHandler : IRequestHandler<GetEduRupAlgorithmByIdQuery, Edu_RupAlgorithmsDto?>
{
    private readonly ISsoRepository<Edu_RupAlgorithms> _repository;

    public GetEduRupAlgorithmByIdQueryHandler(ISsoRepository<Edu_RupAlgorithms> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_RupAlgorithmsDto?> Handle(GetEduRupAlgorithmByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_RupAlgorithmsDto { ID = entity.ID, Title = entity.Title };
    }
}
