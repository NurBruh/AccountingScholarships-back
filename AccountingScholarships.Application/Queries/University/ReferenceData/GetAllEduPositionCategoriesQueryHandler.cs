using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduPositionCategoriesQueryHandler : IRequestHandler<GetAllEduPositionCategoriesQuery, IReadOnlyList<Edu_PositionCategoriesDto>>
{
    private readonly ISsoRepository<Edu_PositionCategories> _repository;

    public GetAllEduPositionCategoriesQueryHandler(ISsoRepository<Edu_PositionCategories> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_PositionCategoriesDto>> Handle(GetAllEduPositionCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_PositionCategoriesDto { ID = e.ID, Title = e.Title }).ToList().AsReadOnly();
    }
}
