using AccountingScholarships.Application.Queries.EpvoSso;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers.Real;

[ApiController]
[Route("api/comparison")]
public class ComparisonController : ControllerBase
{
    private readonly IMediator _mediator;

    public ComparisonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("students")]
    public async Task<IActionResult> GetStudentComparison(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetStudentComparisonQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
}
