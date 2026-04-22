using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetAllEduEducationTypesQueryHandler : IRequestHandler<GetAllEduEducationTypesQuery, IReadOnlyList<Edu_EducationTypesDto>>
{
    private readonly ISsoRepository<Edu_EducationTypes> _repository;
    public GetAllEduEducationTypesQueryHandler(ISsoRepository<Edu_EducationTypes> repository) { _repository = repository; }
    public async Task<IReadOnlyList<Edu_EducationTypesDto>> Handle(GetAllEduEducationTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_EducationTypesDto { ID = e.ID, Title = e.Title, NoBDID = e.NoBDID }).ToList().AsReadOnly();
    }
}
