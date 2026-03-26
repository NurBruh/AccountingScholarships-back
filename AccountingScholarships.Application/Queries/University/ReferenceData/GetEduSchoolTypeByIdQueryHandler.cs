using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduSchoolTypeByIdQueryHandler : IRequestHandler<GetEduSchoolTypeByIdQuery, Edu_SchoolTypesDto?>
{
    private readonly ISsoRepository<Edu_SchoolTypes> _repository;
    public GetEduSchoolTypeByIdQueryHandler(ISsoRepository<Edu_SchoolTypes> repository) { _repository = repository; }
    public async Task<Edu_SchoolTypesDto?> Handle(GetEduSchoolTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (e is null) return null;
        return new Edu_SchoolTypesDto { ID = e.ID, Title = e.Title };
    }
}
