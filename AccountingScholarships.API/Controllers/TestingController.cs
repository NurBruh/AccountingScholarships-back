using AccountingScholarships.Application.Queries.Testing;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers;

/// <summary>
/// Тестовые эндпоинты для интеграции с ЕСУ|ВО.
/// Папка Testing — чтобы не путаться с основным кодом.
/// </summary>
[ApiController]
[Route("api/testing")]
public class TestingController : ControllerBase
{
    private readonly IMediator _mediator;

    public TestingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить данные студентов для экспорта в ЕСУ|ВО
    /// Аналог хранимой процедуры Reload_STUDENT
    /// </summary>
    [HttpGet("export/students")]
    public async Task<IActionResult> GetStudentsForEsuvoExport(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetStudentsForEsuvoExportQuery(), ct);
        return Ok(result);
    }
}
