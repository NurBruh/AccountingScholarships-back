using AccountingScholarships.Application.DTOs;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Features.Grants.Commands;

public class UpdateGrantCommandHandler : IRequestHandler<UpdateGrantCommand, GrantDto?>
{
    private readonly IGrantRepository _grantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGrantCommandHandler(IGrantRepository grantRepository, IUnitOfWork unitOfWork)
    {
        _grantRepository = grantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<GrantDto?> Handle(UpdateGrantCommand request, CancellationToken cancellationToken)
    {
        var grant = await _grantRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (grant is null)
            return null;

        grant.Name = request.Dto.Name;
        grant.Type = request.Dto.Type;
        grant.Amount = request.Dto.Amount;
        grant.StartDate = request.Dto.StartDate;
        grant.EndDate = request.Dto.EndDate;
        grant.IsActive = request.Dto.IsActive;
        grant.UpdatedAt = DateTime.UtcNow;

        await _grantRepository.UpdateAsync(grant, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
