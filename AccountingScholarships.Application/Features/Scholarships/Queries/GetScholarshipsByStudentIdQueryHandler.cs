using AccountingScholarships.Application.DTOs;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Features.Scholarships.Queries;

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
