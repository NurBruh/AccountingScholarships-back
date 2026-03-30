using AccountingScholarships.API.Contracts.Requests;

using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Application.Commands.Scholarships;
using AccountingScholarships.Application.Queries.Scholarships;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers.Testing;

/// <summary>
/// Контроллер для управления стипендиями студентов.
/// </summary>
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

    /// <summary>
    /// Получить стипендию по ее ID.
    /// </summary>
    /// <param name="id">Уникальный идентификатор стипендии.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Данные стипендии.</returns>
    /// <response code="200">Возвращает найденную стипендию.</response>
    /// <response code="401">Необходима авторизация.</response>
    /// <response code="404">Стипендия не найдена.</response>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetScholarshipByIdQuery(id), cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Получить все стипендии конкретного студента.
    /// </summary>
    /// <param name="studentId">Уникальный идентификатор студента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список стипендий студента.</returns>
    /// <response code="200">Возвращает список стипендий студента.</response>
    /// <response code="401">Необходима авторизация.</response>
    [HttpGet("student/{studentId:int}")]
    public async Task<IActionResult> GetByStudentId(int studentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetScholarshipsByStudentIdQuery(studentId), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Назначить новую стипендию студенту.
    /// </summary>
    /// <param name="dto">Данные новой стипендии.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Назначенная стипендия.</returns>
    /// <response code="201">Стипендия успешно назначена.</response>
    /// <response code="400">Ошибки валидации входных данных.</response>
    /// <response code="401">Необходима авторизация.</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateScholarshipRequest dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateScholarshipCommand(dto), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id, version = "1.0" }, result);
    }

    /// <summary>
    /// Обновить данные существующей стипендии.
    /// </summary>
    /// <param name="id">Идентификатор стипендии.</param>
    /// <param name="dto">Новые данные стипендии.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обновленные данные стипендии.</returns>
    /// <response code="200">Стипендия успешно обновлена.</response>
    /// <response code="400">Ошибки валидации входных данных.</response>
    /// <response code="401">Необходима авторизация.</response>
    /// <response code="404">Стипендия не найдена.</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateScholarshipRequest dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateScholarshipCommand(id, dto), cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Удалить стипендию по ID.
    /// </summary>
    /// <param name="id">Идентификатор стипендии.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <response code="204">Стипендия успешно удалена.</response>
    /// <response code="401">Необходима авторизация.</response>
    /// <response code="404">Стипендия не найдена.</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteScholarshipCommand(id), cancellationToken);

        if (!result)
            return NotFound();

        return NoContent();
    }
}

