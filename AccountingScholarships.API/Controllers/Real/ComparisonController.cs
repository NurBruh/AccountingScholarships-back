using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Application.Queries.EpvoSso;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers.Real;

[ApiController]
[Route("api/comparison")]
public class ComparisonController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IChangeLogRepository _changeLogRepo;

    public ComparisonController(IMediator mediator, IChangeLogRepository changeLogRepo)
    {
        _mediator = mediator;
        _changeLogRepo = changeLogRepo;
    }

    [HttpGet("students")]
    public async Task<IActionResult> GetStudentComparison(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] string filter = "all",
        [FromQuery] string? search = null,
        CancellationToken ct = default)
    {
        var result = await _mediator.Send(new GetStudentComparisonQuery
        {
            Page = page,
            PageSize = pageSize,
            Filter = filter,
            Search = search
        }, ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("change-logs")]
    public async Task<IActionResult> GetChangeLogs(
        [FromQuery] string? iin = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken ct = default)
    {
        if (!string.IsNullOrWhiteSpace(iin))
        {
            var logs = await _changeLogRepo.GetChangeLogsByIinAsync(iin.Trim(), ct);
            return Ok(logs);
        }

        var result = await _changeLogRepo.GetChangeLogsAsync(null, page, pageSize, ct);
        return Ok(result);
    }
}
