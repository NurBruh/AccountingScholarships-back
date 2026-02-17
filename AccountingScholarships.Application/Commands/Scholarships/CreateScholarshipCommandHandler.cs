
using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Domain.Entities;
using MediatR;

namespace AccountingScholarships.Application.Commands.Scholarships;

public class CreateScholarshipCommandHandler : IRequestHandler<CreateScholarshipCommand, ScholarshipDto>
{
    private readonly IScholarshipRepository _scholarshipRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateScholarshipCommandHandler(IScholarshipRepository scholarshipRepository, IUnitOfWork unitOfWork)
    {
        _scholarshipRepository = scholarshipRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ScholarshipDto> Handle(CreateScholarshipCommand request, CancellationToken cancellationToken)
    {
        var scholarship = new Scholarship
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

        await _scholarshipRepository.AddAsync(scholarship, cancellationToken);
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
