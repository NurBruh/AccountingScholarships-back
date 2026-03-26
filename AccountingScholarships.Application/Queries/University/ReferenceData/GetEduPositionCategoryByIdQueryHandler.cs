using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetEduPositionCategoryByIdQueryHandler : IRequestHandler<GetEduPositionCategoryByIdQuery, Edu_PositionCategoriesDto?>
{
    private readonly ISsoRepository<Edu_PositionCategories> _repository;

    public GetEduPositionCategoryByIdQueryHandler(ISsoRepository<Edu_PositionCategories> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_PositionCategoriesDto?> Handle(GetEduPositionCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_PositionCategoriesDto { ID = entity.ID, Title = entity.Title };
    }
}
