using AccountingScholarships.Application.DTOs;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Features.Grants.Queries;

public class GetGrantsByStudentIdQueryHandler : IRequestHandler<GetGrantsByStudentIdQuery, IReadOnlyList<GrantDto>>
{
    private readonly IGrantRepository _grantRepository;

    public GetGrantsByStudentIdQueryHandler(IGrantRepository grantRepository)
    {
        _grantRepository = grantRepository;
    }

    public async Task<IReadOnlyList<GrantDto>> Handle(GetGrantsByStudentIdQuery request, CancellationToken cancellationToken)
    {
        var grants = await _grantRepository.GetByStudentIdAsync(request.StudentId, cancellationToken);

        return grants.Select(g => new GrantDto
        {
            Id = g.Id,
            Name = g.Name,
            Type = g.Type,
            Amount = g.Amount,
            StartDate = g.StartDate,
            EndDate = g.EndDate,
            IsActive = g.IsActive,
            StudentId = g.StudentId
        }).ToList();
    }
}
