using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduSchoolRegionStatusByIdQueryHandler : IRequestHandler<GetEduSchoolRegionStatusByIdQuery, Edu_SchoolRegionStatusesDto?>
{
    private readonly ISsoRepository<Edu_SchoolRegionStatuses> _repository;
    public GetEduSchoolRegionStatusByIdQueryHandler(ISsoRepository<Edu_SchoolRegionStatuses> repository) { _repository = repository; }
    public async Task<Edu_SchoolRegionStatusesDto?> Handle(GetEduSchoolRegionStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (e is null) return null;
        return new Edu_SchoolRegionStatusesDto { ID = e.ID, Title = e.Title };
    }
}
