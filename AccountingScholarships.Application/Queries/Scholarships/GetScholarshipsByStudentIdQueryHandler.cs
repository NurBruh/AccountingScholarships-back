
using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.Scholarships;

public class GetScholarshipsByStudentIdQueryHandler : IRequestHandler<GetScholarshipsByStudentIdQuery, IReadOnlyList<ScholarshipDto>>
{
    private readonly IScholarshipRepository _scholarshipRepository;

    public GetScholarshipsByStudentIdQueryHandler(IScholarshipRepository scholarshipRepository)
    {
        _scholarshipRepository = scholarshipRepository;
    }

    public async Task<IReadOnlyList<ScholarshipDto>> Handle(GetScholarshipsByStudentIdQuery request, CancellationToken cancellationToken)
    {
        var scholarships = await _scholarshipRepository.GetByStudentIdAsync(request.StudentId, cancellationToken);

        return scholarships.Select(s => new ScholarshipDto
        {
            Id = s.Id,
            Name = s.Name,
            Type = s.Type,
            Amount = s.Amount,
            StartDate = s.StartDate,
            EndDate = s.EndDate,
            IsActive = s.IsActive,
            StudentId = s.StudentId
        }).ToList();
    }
}
