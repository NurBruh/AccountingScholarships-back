
using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.Grants;

public class GetGrantByIdQueryHandler : IRequestHandler<GetGrantByIdQuery, GrantDto?>
{
    private readonly IGrantRepository _grantRepository;

    public GetGrantByIdQueryHandler(IGrantRepository grantRepository)
    {
        _grantRepository = grantRepository;
    }

    public async Task<GrantDto?> Handle(GetGrantByIdQuery request, CancellationToken cancellationToken)
    {
        var grant = await _grantRepository.GetByIdAsync(request.Id, cancellationToken);

        if (grant is null)
            return null;

        return new GrantDto
        {
            Id = grant.Id,
            Name = grant.Name,
            Type = grant.Type,
            Amount = grant.Amount,
            StartDate = grant.StartDate,
            EndDate = grant.EndDate,
            IsActive = grant.IsActive,
            StudentId = grant.StudentId
        };
    }
}
