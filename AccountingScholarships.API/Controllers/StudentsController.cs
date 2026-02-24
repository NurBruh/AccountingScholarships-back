using AccountingScholarships.API.Contracts.Requests;

using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Application.Commands.Students;
using AccountingScholarships.Application.Queries.Students;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers;

/// <summary>
/// Контроллер для работы со студентами в локальной базе данных (ССО).
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить список всех студентов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список студентов.</returns>
    /// <response code="200">Возвращает список всех студентов.</response>
    /// <response code="401">Необходима авторизация.</response>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllStudentsQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить данные студента по ID.
    /// </summary>
    /// <param name="id">Уникальный идентификатор студента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Данные студента.</returns>
    /// <response code="200">Возвращает найденного студента.</response>
    /// <response code="401">Необходима авторизация.</response>
    /// <response code="404">Студент не найден.</response>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetStudentByIdQuery(id), cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Создать нового студента.
    /// </summary>
    /// <param name="dto">Данные нового студента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданный студент.</returns>
    /// <response code="201">Студент успешно создан.</response>
    /// <response code="400">Ошибки валидации входных данных.</response>
    /// <response code="401">Необходима авторизация.</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentRequest dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateStudentCommand(dto), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id, version = "1.0" }, result);
    }

    /// <summary>
    /// Обновить данные существующего студента.
    /// </summary>
    /// <param name="id">Идентификатор студента для обновления.</param>
    /// <param name="dto">Новые данные студента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обновленные данные студента.</returns>
    /// <response code="200">Студент успешно обновлен.</response>
    /// <response code="400">Ошибки валидации входных данных.</response>
    /// <response code="401">Необходима авторизация.</response>
    /// <response code="404">Студент не найден.</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentRequest dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateStudentCommand(id, dto), cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Удалить студента по ID.
    /// </summary>
    /// <param name="id">Идентификатор студента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <response code="204">Студент успешно удален.</response>
    /// <response code="401">Необходима авторизация.</response>
    /// <response code="404">Студент не найден.</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteStudentCommand(id), cancellationToken);

        if (!result)
            return NotFound();

        return NoContent();
    }
}

