using AccountingScholarships.Application.Common;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Commands.StoredProcedures;

/// <summary>
/// Команда на выполнение хранимой процедуры Reload_STUDENT.
/// </summary>
public record ExecuteReloadStudentCommand() : IRequest<StoredProcedureResult>;

/// <summary>
/// Обработчик команды — вызывает хранимую процедуру через репозиторий.
/// </summary>
public class ExecuteReloadStudentCommandHandler
    : IRequestHandler<ExecuteReloadStudentCommand, StoredProcedureResult>
{
    private readonly IStoredProcedureRepository _repo;

    public ExecuteReloadStudentCommandHandler(IStoredProcedureRepository repo)
    {
        _repo = repo;
    }

    public async Task<StoredProcedureResult> Handle(
        ExecuteReloadStudentCommand request,
        CancellationToken cancellationToken)
    {
        return await _repo.ExecuteReloadStudentAsync(cancellationToken);
    }
}
