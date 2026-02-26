using AccountingScholarships.API.Contracts.Requests;

using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Application.Commands.Epvo;
using AccountingScholarships.Application.Queries.Epvo;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers;

/// <summary>
/// Контроллер для синхронизации данных студентов между ССО и ЕПВО.
/// </summary>
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

    /// <summary>
    /// Получить всех студентов из базы ЕПВО.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список студентов ЕПВО.</returns>
    /// <response code="200">Возвращает всех студентов из ЕПВО.</response>
    /// <response code="401">Необходима авторизация.</response>
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

    /// <summary>
    /// Запустить полную синхронизацию студентов из ССО в ЕПВО.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Количество синхронизированных студентов.</returns>
    /// <response code="200">Возвращает результат синхронизации.</response>
    /// <response code="401">Необходима авторизация.</response>
    [HttpPost("sync-to-epvo")]
    public async Task<IActionResult> SyncToEpvo(CancellationToken cancellationToken)
    {
        var syncedCount = await _mediator.Send(new SyncStudentsToEpvoCommand(), cancellationToken);
        return Ok(new { SyncedCount = syncedCount, Message = $"Синхронизировано {syncedCount} студентов в ЕПВО." });
    }

    /// <summary>
    /// Получить сравнение данных между ССО и ЕПВО.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат сравнения данных.</returns>
    /// <response code="200">Возвращает данные о расхождениях между ССО и ЕПВО.</response>
    /// <response code="401">Необходима авторизация.</response>
    [HttpGet("compare")]
    public async Task<IActionResult> GetSsoEpvoComparison(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetSsoEpvoComparisonQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Синхронизировать конкретного студента по ИИН.
    /// </summary>
    /// <param name="iin">ИИН студента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Сообщение о результате.</returns>
    /// <response code="200">Студент успешно синхронизирован.</response>
    /// <response code="401">Необходима авторизация.</response>
    /// <response code="404">Студент не найден.</response>
    [HttpPost("sync-student/{iin}")]
    public async Task<IActionResult> SyncStudentToEpvo(string iin, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SyncStudentToEpvoCommand(iin), cancellationToken);
        if (!result) return NotFound(new { Message = $"Студент с ИИН {iin} не найден в посреднике." });
        return Ok(new { Message = $"Студент с ИИН {iin} успешно синхронизирован в ЕПВО." });
    }

    /// <summary>
    /// Выполнить пакетную синхронизацию выбранных студентов по ИИН.
    /// </summary>
    /// <param name="request">Запрос со списком ИИН.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Количество синхронизированных студентов.</returns>
    /// <response code="200">Возвращает результат пакетной синхронизации.</response>
    /// <response code="401">Необходима авторизация.</response>
    [HttpPost("sync-batch")]
    public async Task<IActionResult> SyncBatchToEpvo([FromBody] SyncBatchRequest request, CancellationToken cancellationToken)
    {
        var syncedCount = await _mediator.Send(new SendSelectedStudentsToEpvoCommand(request.IINs), cancellationToken);
        return Ok(new { SyncedCount = syncedCount, Message = $"Синхронизировано {syncedCount} студентов в ЕПВО." });
    }

    /// <summary>
    /// Синхронизировать только измененных студентов в ЕПВО.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Количество синхронизированных студентов.</returns>
    /// <response code="200">Возвращает результат синхронизации измененных студентов.</response>
    /// <response code="401">Необходима авторизация.</response>
    [HttpPost("sync-changed")]
    public async Task<IActionResult> SyncChangedToEpvo(CancellationToken cancellationToken)
    {
        var syncedCount = await _mediator.Send(new SyncChangedStudentsToEpvoCommand(), cancellationToken);
        return Ok(new { SyncedCount = syncedCount, Message = $"Синхронизировано {syncedCount} изменённых студентов в ЕПВО (массив)." });
    }

    /// <summary>
    /// Обновить расчетный счет (IBAN) студента по ИИН.
    /// </summary>
    /// <param name="iin">ИИН студента.</param>
    /// <param name="request">Запрос с новым IBAN.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Сообщение об обновлении.</returns>
    /// <response code="200">Счет успешно обновлен.</response>
    /// <response code="401">Необходима авторизация.</response>
    /// <response code="404">Студент не найден.</response>
    [HttpPatch("students/{iin}/iban")]
    public async Task<IActionResult> UpdateStudentIban(string iin, [FromBody] UpdateIbanRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateStudentIbanCommand(iin, request.NewIban), cancellationToken);
        if (!result) return NotFound(new { Message = $"Студент с ИИН {iin} не найден." });
        return Ok(new { Message = $"Расчётный счёт студента с ИИН {iin} обновлён в ССО. Актуализируйте данные в ССО vs ЕПВО." });
    }
}

public record SyncBatchRequest(List<string> IINs);
public record UpdateIbanRequest(string NewIban);