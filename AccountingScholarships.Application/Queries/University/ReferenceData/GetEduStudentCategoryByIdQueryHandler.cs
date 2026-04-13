using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetEduStudentCategoryByIdQueryHandler : IRequestHandler<GetEduStudentCategoryByIdQuery, Edu_StudentCategoriesDto?>
{
    private readonly ISsoRepository<Edu_StudentCategories> _repository;

    public GetEduStudentCategoryByIdQueryHandler(ISsoRepository<Edu_StudentCategories> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_StudentCategoriesDto?> Handle(GetEduStudentCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_StudentCategoriesDto { ID = entity.ID, Title = entity.Title };
    }
}
