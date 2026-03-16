
using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Domain.Entities.Grants;
using MediatR;

namespace AccountingScholarships.Application.Commands.Grants;

public class CreateGrantCommandHandler : IRequestHandler<CreateGrantCommand, GrantDto>
{
    private readonly IGrantRepository _grantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGrantCommandHandler(IGrantRepository grantRepository, IUnitOfWork unitOfWork)
    {
        _grantRepository = grantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<GrantDto> Handle(CreateGrantCommand request, CancellationToken cancellationToken)
    {
        var grant = new Grant
        {
            Name = request.Dto.Name,
            Type = request.Dto.Type,
            Amount = request.Dto.Amount,
            StartDate = request.Dto.StartDate,
            EndDate = request.Dto.EndDate,
            IsActive = request.Dto.IsActive,
            StudentId = request.Dto.StudentId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _grantRepository.AddAsync(grant, cancellationToken);
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
