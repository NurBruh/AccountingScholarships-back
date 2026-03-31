using AccountingScholarships.Application.Queries.ChangeHistory;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers.Testing;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class ChangeHistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChangeHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить историю изменений. Опционально — по ИИН студента.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetHistory([FromQuery] string? iin, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetChangeHistoryQuery(iin), cancellationToken);
        return Ok(result);
    }
}
