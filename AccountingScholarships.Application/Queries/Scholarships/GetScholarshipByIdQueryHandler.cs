
using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.Scholarships;

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
            //EndDate = scholarship.EndDate,
            LostDate = scholarship.LostDate,
            OrderLostDate = scholarship.OrderLostDate,
            OrderCandidateDate = scholarship.OrderCandidateDate,
            Notes = scholarship.Notes,
            IsActive = scholarship.IsActive,
            StudentId = scholarship.StudentId
        };
    }
}
