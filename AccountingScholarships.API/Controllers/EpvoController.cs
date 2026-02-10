using AccountingScholarships.Application.DTOs;
using AccountingScholarships.Application.Features.Epvo.Commands;
using AccountingScholarships.Application.Features.Epvo.Queries;
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
    public async Task<ActionResult<IReadOnlyList<EpvoStudentDto>>> GetAllEpvoStudents(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllEpvoStudentsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost("sync")]
    public async Task<ActionResult<object>> SyncFromEpvo(CancellationToken cancellationToken)
    {
        var syncedCount = await _mediator.Send(new SyncStudentsFromEpvoCommand(), cancellationToken);
        return Ok(new { SyncedCount = syncedCount, Message = $"Синхронизировано {syncedCount} студентов из ЕПВО." });
    }
}
