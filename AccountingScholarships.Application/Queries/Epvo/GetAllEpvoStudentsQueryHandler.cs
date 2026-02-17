
using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.Epvo;

public class GetAllEpvoStudentsQueryHandler : IRequestHandler<GetAllEpvoStudentsQuery, IReadOnlyList<EpvoStudentDto>>
{
    private readonly IEpvoRepository _epvoRepository;

    public GetAllEpvoStudentsQueryHandler(IEpvoRepository epvoRepository)
    {
        _epvoRepository = epvoRepository;
    }

    public async Task<IReadOnlyList<EpvoStudentDto>> Handle(GetAllEpvoStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _epvoRepository.GetAllAsync(cancellationToken);

        return students.Select(s => new EpvoStudentDto
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            MiddleName = s.MiddleName,
            IIN = s.IIN,
            DateOfBirth = s.DateOfBirth,
            Faculty = s.Faculty,
            Speciality = s.Speciality,
            Course = s.Course,
            GrantName = s.GrantName,
            GrantAmount = s.GrantAmount,
            ScholarshipName = s.ScholarshipName,
            ScholarshipAmount = s.ScholarshipAmount,
            IsActive = s.IsActive,
            SyncDate = s.SyncDate
        }).ToList().AsReadOnly();
    }
}
