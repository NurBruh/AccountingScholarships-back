using AccountingScholarships.API.Contracts.Requests;

using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Application.Commands.Grants;
using AccountingScholarships.Application.Queries.Grants;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers.Testing;

/// <summary>
/// Контроллер для управления грантами студентов.
/// </summary>
[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class GrantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GrantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить грант по его ID.
    /// </summary>
    /// <param name="id">Уникальный идентификатор гранта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Данные гранта.</returns>
    /// <response code="200">Возвращает найденный грант.</response>
    /// <response code="401">Необходима авторизация.</response>
    /// <response code="404">Грант не найден.</response>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetGrantByIdQuery(id), cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Получить все гранты конкретного студента.
    /// </summary>
    /// <param name="studentId">Уникальный идентификатор студента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список грантов студента.</returns>
    /// <response code="200">Возвращает список грантов студента.</response>
    /// <response code="401">Необходима авторизация.</response>
    [HttpGet("student/{studentId:int}")]
    public async Task<IActionResult> GetByStudentId(int studentId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetGrantsByStudentIdQuery(studentId), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Добавить новый грант студенту.
    /// </summary>
    /// <param name="dto">Данные нового гранта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданный грант.</returns>
    /// <response code="201">Грант успешно создан.</response>
    /// <response code="400">Ошибки валидации входных данных.</response>
    /// <response code="401">Необходима авторизация.</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGrantRequest dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateGrantCommand(dto), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id, version = "2.0" }, result);
    }

    /// <summary>
    /// Обновить данные существующего гранта.
    /// </summary>
    /// <param name="id">Идентификатор гранта.</param>
    /// <param name="dto">Новые данные гранта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обновленные данные гранта.</returns>
    /// <response code="200">Грант успешно обновлен.</response>
    /// <response code="400">Ошибки валидации входных данных.</response>
    /// <response code="401">Необходима авторизация.</response>
    /// <response code="404">Грант не найден.</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateGrantRequest dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateGrantCommand(id, dto), cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Удалить грант по ID.
    /// </summary>
    /// <param name="id">Идентификатор гранта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <response code="204">Грант успешно удален.</response>
    /// <response code="401">Необходима авторизация.</response>
    /// <response code="404">Грант не найден.</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteGrantCommand(id), cancellationToken);

        if (!result)
            return NotFound();

        return NoContent();
    }
}

