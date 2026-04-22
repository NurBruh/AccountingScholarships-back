using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduCitizenCategoryByIdQueryHandler : IRequestHandler<GetEduCitizenCategoryByIdQuery, Edu_CitizenCategoriesDto?>
{
    private readonly ISsoRepository<Edu_CitizenCategories> _repository;
    public GetEduCitizenCategoryByIdQueryHandler(ISsoRepository<Edu_CitizenCategories> repository) { _repository = repository; }
    public async Task<Edu_CitizenCategoriesDto?> Handle(GetEduCitizenCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_CitizenCategoriesDto { ID = entity.ID, Title = entity.Title };
    }
}
