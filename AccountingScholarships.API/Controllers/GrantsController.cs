using AccountingScholarships.Application.DTOs;
using AccountingScholarships.Application.Features.Grants.Commands;
using AccountingScholarships.Application.Features.Grants.Queries;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class GrantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GrantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GrantDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetGrantByIdQuery(id), cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("student/{studentId:guid}")]
    public async Task<ActionResult<IReadOnlyList<GrantDto>>> GetByStudentId(Guid studentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetGrantsByStudentIdQuery(studentId), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GrantDto>> Create([FromBody] CreateGrantDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateGrantCommand(dto), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id, version = "1.0" }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<GrantDto>> Update(Guid id, [FromBody] UpdateGrantDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateGrantCommand(id, dto), cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteGrantCommand(id), cancellationToken);

        if (!result)
            return NotFound();

        return NoContent();
    }
}
