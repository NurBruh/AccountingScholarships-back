using AccountingScholarships.Application.DTOs;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Features.Scholarships.Queries;

public class GetScholarshipByIdQueryHandler : IRequestHandler<GetScholarshipByIdQuery, ScholarshipDto?>
{
    private readonly IScholarshipRepository _scholarshipRepository;

    public GetScholarshipByIdQueryHandler(IScholarshipRepository scholarshipRepository)
    {
        _scholarshipRepository = scholarshipRepository;
    }

    public async Task<ScholarshipDto?> Handle(GetScholarshipByIdQuery request, CancellationToken cancellationToken)
    {
        var scholarship = await _scholarshipRepository.GetByIdAsync(request.Id, cancellationToken);

        if (scholarship is null)
            return null;

        return new ScholarshipDto
        {
            Id = scholarship.Id,
            Name = scholarship.Name,
            Type = scholarship.Type,
            Amount = scholarship.Amount,
            StartDate = scholarship.StartDate,
            EndDate = scholarship.EndDate,
            IsActive = scholarship.IsActive,
            StudentId = scholarship.StudentId
        };
    }
}
