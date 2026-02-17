using AccountingScholarships.API.Contracts.Requests;

using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Application.Commands.Scholarships;
using AccountingScholarships.Application.Queries.Scholarships;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class ScholarshipsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ScholarshipsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ScholarshipDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetScholarshipByIdQuery(id), cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("student/{studentId:guid}")]
    public async Task<ActionResult<IReadOnlyList<ScholarshipDto>>> GetByStudentId(Guid studentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetScholarshipsByStudentIdQuery(studentId), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ScholarshipDto>> Create([FromBody] CreateScholarshipRequest dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateScholarshipCommand(dto), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id, version = "1.0" }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ScholarshipDto>> Update(Guid id, [FromBody] UpdateScholarshipRequest dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateScholarshipCommand(id, dto), cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteScholarshipCommand(id), cancellationToken);

        if (!result)
            return NotFound();

        return NoContent();
    }
}

