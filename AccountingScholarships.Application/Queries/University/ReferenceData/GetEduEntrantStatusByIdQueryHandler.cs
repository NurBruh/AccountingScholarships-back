using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduEntrantStatusByIdQueryHandler : IRequestHandler<GetEduEntrantStatusByIdQuery, Edu_EntrantStatusesDto?>
{
    private readonly ISsoRepository<Edu_EntrantStatuses> _repository;
    public GetEduEntrantStatusByIdQueryHandler(ISsoRepository<Edu_EntrantStatuses> repository) { _repository = repository; }
    public async Task<Edu_EntrantStatusesDto?> Handle(GetEduEntrantStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (e is null) return null;
        return new Edu_EntrantStatusesDto { ID = e.ID, Title = e.Title };
    }
}
