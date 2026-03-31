using AccountingScholarships.Application.Queries.ReferenceData;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers.Testing;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ReferenceDataController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReferenceDataController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить все справочные данные (институты, кафедры, специальности, формы обучения и т.д.)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllReferenceDataQuery(), cancellationToken);
        return Ok(result);
    }
}
