using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetAllEduCitizenCategoriesQueryHandler : IRequestHandler<GetAllEduCitizenCategoriesQuery, IReadOnlyList<Edu_CitizenCategoriesDto>>
{
    private readonly ISsoRepository<Edu_CitizenCategories> _repository;
    public GetAllEduCitizenCategoriesQueryHandler(ISsoRepository<Edu_CitizenCategories> repository) { _repository = repository; }
    public async Task<IReadOnlyList<Edu_CitizenCategoriesDto>> Handle(GetAllEduCitizenCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_CitizenCategoriesDto { ID = e.ID, Title = e.Title }).ToList().AsReadOnly();
    }
}
