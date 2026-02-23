using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

public class UpdateStudentIbanCommandHandler : IRequestHandler<UpdateStudentIbanCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPosrednikRepository _posrednikRepository;
    private readonly IEpvoRepository _epvoRepository;
    private readonly IEpvoApiClient _epvoApiClient;

    public UpdateStudentIbanCommandHandler(
        IUnitOfWork unitOfWork,
        IPosrednikRepository posrednikRepository,
        IEpvoRepository epvoRepository,
        IEpvoApiClient epvoApiClient)
    {
        _unitOfWork = unitOfWork;
        _posrednikRepository = posrednikRepository;
        _epvoRepository = epvoRepository;
        _epvoApiClient = epvoApiClient;
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

        // 3. Отправляем в ЕПВО как массив из 1 студента (через клиент)
        var payload = new List<EpvoSendPayloadDto>
        {
            new EpvoSendPayloadDto
            {
                IIN               = posrednik?.IIN ?? iin,
                FirstName         = posrednik?.FirstName ?? student.FirstName,
                LastName          = posrednik?.LastName ?? student.LastName,
                MiddleName        = posrednik?.MiddleName ?? student.MiddleName,
                Faculty           = posrednik?.Faculty ?? student.Faculty,
                Speciality        = posrednik?.Speciality ?? student.Speciality,
                Course            = posrednik?.Course ?? student.Course,
                GrantName         = posrednik?.GrantName,
                GrantAmount       = posrednik?.GrantAmount ?? 0,
                ScholarshipName   = posrednik?.ScholarshipName,
                ScholarshipAmount = posrednik?.ScholarshipAmount,
                iban              = newIban,
                isActive          = posrednik?.IsActive ?? student.IsActive
            }
        };

        await _epvoApiClient.SendStudentsAsync(payload, cancellationToken);

        return true;
    }
}

