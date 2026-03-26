using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduSchoolSubjectsQueryHandler : IRequestHandler<GetAllEduSchoolSubjectsQuery, IReadOnlyList<Edu_SchoolSubjectsDto>>
{
    private readonly ISsoRepository<Edu_SchoolSubjects> _repository;

    public GetAllEduSchoolSubjectsQueryHandler(ISsoRepository<Edu_SchoolSubjects> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_SchoolSubjectsDto>> Handle(GetAllEduSchoolSubjectsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_SchoolSubjectsDto { ID = e.ID, Title = e.Title, Number = e.Number, IsRequired = e.IsRequired }).ToList().AsReadOnly();
    }
}
