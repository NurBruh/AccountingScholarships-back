using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduStudentCategoriesQueryHandler : IRequestHandler<GetAllEduStudentCategoriesQuery, IReadOnlyList<Edu_StudentCategoriesDto>>
{
    private readonly ISsoRepository<Edu_StudentCategories> _repository;

    public GetAllEduStudentCategoriesQueryHandler(ISsoRepository<Edu_StudentCategories> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_StudentCategoriesDto>> Handle(GetAllEduStudentCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_StudentCategoriesDto { ID = e.ID, Title = e.Title }).ToList().AsReadOnly();
    }
}
