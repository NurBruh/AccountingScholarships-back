using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetAllEduStudentStatusesQueryHandler : IRequestHandler<GetAllEduStudentStatusesQuery, IReadOnlyList<Edu_StudentStatusesDto>>
{
    private readonly ISsoRepository<Edu_StudentStatuses> _repository;

    public GetAllEduStudentStatusesQueryHandler(ISsoRepository<Edu_StudentStatuses> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Edu_StudentStatusesDto>> Handle(GetAllEduStudentStatusesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_StudentStatusesDto { ID = e.ID, Title = e.Title, NOBDID = e.NOBDID }).ToList().AsReadOnly();
    }
}
