using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public class GetEduStudentStatusByIdQueryHandler : IRequestHandler<GetEduStudentStatusByIdQuery, Edu_StudentStatusesDto?>
{
    private readonly ISsoRepository<Edu_StudentStatuses> _repository;

    public GetEduStudentStatusByIdQueryHandler(ISsoRepository<Edu_StudentStatuses> repository)
    {
        _repository = repository;
    }

    public async Task<Edu_StudentStatusesDto?> Handle(GetEduStudentStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_StudentStatusesDto { ID = entity.ID, Title = entity.Title, NOBDID = entity.NOBDID };
    }
}
