using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetEduSchoolSubjectByIdQueryHandler : IRequestHandler<GetEduSchoolSubjectByIdQuery, Edu_SchoolSubjectsDto?>
{
    private readonly ISsoRepository<Edu_SchoolSubjects> _repository;

    public GetEduSchoolSubjectByIdQueryHandler(ISsoRepository<Edu_SchoolSubjects> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_SchoolSubjectsDto?> Handle(GetEduSchoolSubjectByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_SchoolSubjectsDto { ID = entity.ID, Title = entity.Title, Number = entity.Number, IsRequired = entity.IsRequired };
    }
}
