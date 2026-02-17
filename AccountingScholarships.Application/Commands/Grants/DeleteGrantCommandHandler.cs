using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.Grants;

public class DeleteGrantCommandHandler : IRequestHandler<DeleteGrantCommand, bool>
{
    private readonly IGrantRepository _grantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGrantCommandHandler(IGrantRepository grantRepository, IUnitOfWork unitOfWork)
    {
        _grantRepository = grantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteGrantCommand request, CancellationToken cancellationToken)
    {
        var grant = await _grantRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (grant is null)
            return false;

        await _grantRepository.DeleteAsync(grant, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
