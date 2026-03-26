using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduSemesterTypeByIdQueryHandler : IRequestHandler<GetEduSemesterTypeByIdQuery, Edu_SemesterTypesDto?>
{
    private readonly ISsoRepository<Edu_SemesterTypes> _repository;
    public GetEduSemesterTypeByIdQueryHandler(ISsoRepository<Edu_SemesterTypes> repository) { _repository = repository; }
    public async Task<Edu_SemesterTypesDto?> Handle(GetEduSemesterTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (e is null) return null;
        return new Edu_SemesterTypesDto { ID = e.ID, Title = e.Title, OrderBy = e.OrderBy };
    }
}
