using Microsoft.AspNetCore.Mvc;
using MediatR;
using AccountingScholarships.Application.Queries.University.Students;

namespace AccountingScholarships.API.Controllers.Real;

/// <summary>
/// Тестовый контроллер для проверки данных из SSO базы (Edu_Students + Edu_Users + справочники).
/// </summary>
[ApiController]
[Route("api/sso-test")]
public class SsoTestController : ControllerBase
{
    private readonly IMediator _mediator;

    public SsoTestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить всех студентов из SSO БД (с Include по всем FK).
    /// </summary>
    [HttpGet("students")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var students = await _mediator.Send(new GetAllEduStudentsQuery(), cancellationToken);
        return Ok(students);
    }

    /// <summary>
    /// Получить студента по StudentID из SSO БД.
    /// </summary>
    [HttpGet("students/{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var student = await _mediator.Send(new GetEduStudentByIdQuery(id), cancellationToken);
        if (student is null)
            return NotFound(new { message = $"Студент с ID={id} не найден в SSO БД." });

        return Ok(student);
    }

    /// <summary>
    /// Получить студента по ИИН из SSO БД.
    /// </summary>
    [HttpGet("students/by-iin/{iin}")]
    public async Task<IActionResult> GetByIIN(string iin, CancellationToken cancellationToken)
    {
        var student = await _mediator.Send(new GetEduStudentByIINQuery(iin), cancellationToken);
        if (student is null)
            return NotFound(new { message = $"Студент с ИИН={iin} не найден в SSO БД." });

        return Ok(student);
    }

    /// <summary>
    /// Получить список студентов (JOIN c EduUsers) из локальной SSO базы.
    /// </summary>
    [HttpGet("sso-top")]
    public async Task<IActionResult> GetAllSsoStudents(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllSsoStudentsQuery(), cancellationToken);
        return Ok(result);
    }
}
