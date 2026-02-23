using AccountingScholarships.API.Contracts.Requests;

using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Application.Commands.Epvo;
using AccountingScholarships.Application.Queries.Epvo;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class EpvoController : ControllerBase
{
    private readonly IMediator _mediator;

    public EpvoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("students")]
    public async Task<IActionResult> GetAllEpvoStudents(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllEpvoStudentsQuery(), cancellationToken);
        return Ok(result);
    }

    //[HttpPost("sync")]
    //public async Task<IActionResult> SyncFromEpvo(CancellationToken cancellationToken)
    //{
    //    var syncedCount = await _mediator.Send(new SyncStudentsFromEpvoCommand(), cancellationToken);
    //    return Ok(new { SyncedCount = syncedCount, Message = $"Синхронизировано {syncedCount} студентов из ЕПВО." });
    //}

    [HttpPost("sync-to-epvo")]
    public async Task<IActionResult> SyncToEpvo(CancellationToken cancellationToken)
    {
        var syncedCount = await _mediator.Send(new SyncStudentsToEpvoCommand(), cancellationToken);
        return Ok(new { SyncedCount = syncedCount, Message = $"Синхронизировано {syncedCount} студентов в ЕПВО." });
    }

    [HttpGet("compare")]
    public async Task<IActionResult> GetSsoEpvoComparison(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetSsoEpvoComparisonQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost("sync-student/{iin}")]
    public async Task<IActionResult> SyncStudentToEpvo(string iin, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SyncStudentToEpvoCommand(iin), cancellationToken);
        if (!result) return NotFound(new { Message = $"Студент с ИИН {iin} не найден в посреднике." });
        return Ok(new { Message = $"Студент с ИИН {iin} успешно синхронизирован в ЕПВО." });
    }

    [HttpPost("sync-batch")]
    public async Task<IActionResult> SyncBatchToEpvo([FromBody] SyncBatchRequest request, CancellationToken cancellationToken)
    {
        var syncedCount = await _mediator.Send(new SendSelectedStudentsToEpvoCommand(request.IINs), cancellationToken);
        return Ok(new { SyncedCount = syncedCount, Message = $"Синхронизировано {syncedCount} студентов в ЕПВО." });
    }

    [HttpPost("sync-changed")]
    public async Task<IActionResult> SyncChangedToEpvo(CancellationToken cancellationToken)
    {
        var syncedCount = await _mediator.Send(new SyncChangedStudentsToEpvoCommand(), cancellationToken);
        return Ok(new { SyncedCount = syncedCount, Message = $"Синхронизировано {syncedCount} изменённых студентов в ЕПВО (массив)." });
    }

    [HttpPatch("students/{iin}/iban")]
    public async Task<IActionResult> UpdateStudentIban(string iin, [FromBody] UpdateIbanRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateStudentIbanCommand(iin, request.NewIban), cancellationToken);
        if (!result) return NotFound(new { Message = $"Студент с ИИН {iin} не найден в ЕПВО." });
        return Ok(new { Message = $"Расчётный счёт студента с ИИН {iin} успешно обновлён (отправлен в ЕПВО как массив из 1 записи)." });
    }
}

public record SyncBatchRequest(List<string> IINs);
public record UpdateIbanRequest(string NewIban);