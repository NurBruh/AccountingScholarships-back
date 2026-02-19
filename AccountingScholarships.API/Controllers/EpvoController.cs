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
}