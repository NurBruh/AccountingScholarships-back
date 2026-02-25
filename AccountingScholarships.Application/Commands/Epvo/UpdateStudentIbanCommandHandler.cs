using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

public class UpdateStudentIbanCommandHandler : IRequestHandler<UpdateStudentIbanCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPosrednikRepository _posrednikRepository;

    public UpdateStudentIbanCommandHandler(
        IUnitOfWork unitOfWork,
        IPosrednikRepository posrednikRepository)
    {
        _unitOfWork = unitOfWork;
        _posrednikRepository = posrednikRepository;
    }

    public async Task<bool> Handle(UpdateStudentIbanCommand request, CancellationToken cancellationToken)
    {
        var iin = request.IIN;
        var newIban = request.NewIban;

        // 1. Обновляем Student (внутренняя ССО)
        var student = await _unitOfWork.Students.GetByIINAsync(iin, cancellationToken);
        if (student is null) return false;

        student.iban = newIban;
        await _unitOfWork.Students.UpdateAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 2. Обновляем EpvoPosrednik (посредник)
        var posrednik = await _posrednikRepository.GetByIINAsync(iin, cancellationToken);
        if (posrednik is not null)
        {
            posrednik.iban = newIban;
            await _posrednikRepository.UpdateAsync(posrednik, cancellationToken);
            await _posrednikRepository.SaveChangesAsync(cancellationToken);
        }

        // В ЕПВО НЕ отправляем — пользователь сам актуализирует через "ССО vs ЕПВО"
        return true;
    }
}

