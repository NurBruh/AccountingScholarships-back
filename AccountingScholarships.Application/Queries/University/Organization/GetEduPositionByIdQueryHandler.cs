using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public class GetEduPositionByIdQueryHandler : IRequestHandler<GetEduPositionByIdQuery, Edu_PositionsDto?>
{
    private readonly ISsoRepository<Edu_Positions> _repository;

    public GetEduPositionByIdQueryHandler(ISsoRepository<Edu_Positions> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_PositionsDto?> Handle(GetEduPositionByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.FindFirstWithIncludesAsync(x => x.ID == request.Id, new[] { "Category" }, cancellationToken);
        if (e is null) return null;

        return new Edu_PositionsDto
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
        };
    }
}
