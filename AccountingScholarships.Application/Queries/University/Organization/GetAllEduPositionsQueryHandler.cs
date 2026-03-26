using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public class GetAllEduPositionsQueryHandler : IRequestHandler<GetAllEduPositionsQuery, IReadOnlyList<Edu_PositionsDto>>
{
    private readonly ISsoRepository<Edu_Positions> _repository;

    public GetAllEduPositionsQueryHandler(ISsoRepository<Edu_Positions> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_PositionsDto>> Handle(GetAllEduPositionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllWithIncludesAsync(new[] { "Category" }, cancellationToken);

        return entities.Select(e => new Edu_PositionsDto
        {
            ID = e.ID,
            Title = e.Title,
            Deleted = e.Deleted,
            Description = e.Description,
            Lectures = e.Lectures,
            Practices = e.Practices,
            Labs = e.Labs,
            CategoryID = e.CategoryID,
            Category = e.Category == null ? null : new Edu_PositionCategoriesDto
            {
                ID = e.Category.ID,
                Title = e.Category.Title
            }
        }).ToList().AsReadOnly();
    }
}
