using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

public class UpdateStudentIbanCommandHandler : IRequestHandler<UpdateStudentIbanCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStudentIbanCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateStudentIbanCommand request, CancellationToken cancellationToken)
    {
        var student = await _unitOfWork.Students.GetByIINAsync(request.IIN, cancellationToken);
        if (student is null) return false;

        student.iban = request.NewIban;
        await _unitOfWork.Students.UpdateAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}

