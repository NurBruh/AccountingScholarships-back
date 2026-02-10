using AccountingScholarships.Application.DTOs;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Features.Scholarships.Commands;

public class UpdateScholarshipCommandHandler : IRequestHandler<UpdateScholarshipCommand, ScholarshipDto?>
{
    private readonly IScholarshipRepository _scholarshipRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateScholarshipCommandHandler(IScholarshipRepository scholarshipRepository, IUnitOfWork unitOfWork)
    {
        _scholarshipRepository = scholarshipRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ScholarshipDto?> Handle(UpdateScholarshipCommand request, CancellationToken cancellationToken)
    {
        var scholarship = await _scholarshipRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (scholarship is null)
            return null;

        scholarship.Name = request.Dto.Name;
        scholarship.Type = request.Dto.Type;
        scholarship.Amount = request.Dto.Amount;
        scholarship.StartDate = request.Dto.StartDate;
        scholarship.EndDate = request.Dto.EndDate;
        scholarship.IsActive = request.Dto.IsActive;
        scholarship.UpdatedAt = DateTime.UtcNow;

        await _scholarshipRepository.UpdateAsync(scholarship, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
