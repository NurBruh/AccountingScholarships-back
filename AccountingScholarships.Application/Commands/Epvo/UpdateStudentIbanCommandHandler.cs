using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

public class UpdateStudentIbanCommandHandler : IRequestHandler<UpdateStudentIbanCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPosrednikRepository _posrednikRepository;
    private readonly IEpvoRepository _epvoRepository;

    public UpdateStudentIbanCommandHandler(
        IUnitOfWork unitOfWork,
        IPosrednikRepository posrednikRepository,
        IEpvoRepository epvoRepository)
    {
        _unitOfWork = unitOfWork;
        _posrednikRepository = posrednikRepository;
        _epvoRepository = epvoRepository;
    }

    public async Task<bool> Handle(UpdateStudentIbanCommand request, CancellationToken cancellationToken)
    {
        var iin = request.IIN;
        var newIban = request.NewIban;

        // 1. Обновляем Student (внутренняя ССО) — источник для RefreshPosrednikFromSso
        var student = await _unitOfWork.Students.GetByIINAsync(iin, cancellationToken);
        if (student is null) return false;

        student.iban = newIban;
        await _unitOfWork.Students.UpdateAsync(student, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 2. Обновляем EpvoPosrednik (посредник / SSO-сторона сравнения)
        var posrednik = await _posrednikRepository.GetByIINAsync(iin, cancellationToken);
        if (posrednik is not null)
        {
            posrednik.iban = newIban;
            await _posrednikRepository.UpdateAsync(posrednik, cancellationToken);
        }

        // 3. Обновляем EpvoStudent (ЕПВО) — чтобы не было конфликта в сравнении
        var epvoStudent = await _epvoRepository.GetByIINAsync(iin, cancellationToken);
        if (epvoStudent is not null)
        {
            epvoStudent.iban = newIban;
            epvoStudent.SyncDate = DateTime.UtcNow;
            await _epvoRepository.UpdateAsync(epvoStudent, cancellationToken);
        }

        return true;
    }
}
