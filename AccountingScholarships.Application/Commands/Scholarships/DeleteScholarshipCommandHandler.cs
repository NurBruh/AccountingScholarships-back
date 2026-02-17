using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.Scholarships;

public class DeleteScholarshipCommandHandler : IRequestHandler<DeleteScholarshipCommand, bool>
{
    private readonly IScholarshipRepository _scholarshipRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteScholarshipCommandHandler(IScholarshipRepository scholarshipRepository, IUnitOfWork unitOfWork)
    {
        _scholarshipRepository = scholarshipRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteScholarshipCommand request, CancellationToken cancellationToken)
    {
        var scholarship = await _scholarshipRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (scholarship is null)
            return false;

        await _scholarshipRepository.DeleteAsync(scholarship, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
