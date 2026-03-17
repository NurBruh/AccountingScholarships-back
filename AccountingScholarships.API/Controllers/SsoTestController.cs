using AccountingScholarships.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AccountingScholarships.Application.Queries.University.Students;

namespace AccountingScholarships.API.Controllers;

/// <summary>
/// Тестовый контроллер для проверки данных из SSO базы (Edu_Students + Edu_Users + справочники).
/// </summary>
[ApiController]
[Route("api/sso-test")]
public class SsoTestController : ControllerBase
{
    private readonly IEduStudentRepository _repo;
    private readonly IMediator _mediator;

    public SsoTestController(IEduStudentRepository repo, IMediator mediator)
    {
        _repo = repo;
        _mediator = mediator;
    }

    /// <summary>
    /// Получить всех студентов из SSO БД (с Include по всем FK).
    /// </summary>
    [HttpGet("students")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var students = await _repo.GetAllAsDtoAsync(cancellationToken);
        return Ok(students);
    }

    /// <summary>
    /// Получить студента по StudentID из SSO БД.
    /// </summary>
    [HttpGet("students/{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var student = await _repo.GetAsDtoAsync(id, cancellationToken);
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
        var student = await _repo.GetByIINAsync(iin, cancellationToken);
        if (student is null)
            return NotFound(new { message = $"Студент с ИИН={iin} не найден в SSO БД." });

        return Ok(student);
    }

    /// <summary>
    /// Пример CQRS: Получить список студентов (JOIN c EduUsers) из локальной SSO базы, как просил пользователь.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список студентов.</returns>
    [HttpGet("sso-top")]
    public async Task<IActionResult> GetAllSsoStudents(CancellationToken cancellationToken)
    {
        
        var result = await _mediator.Send(new GetAllSsoStudentsQuery(), cancellationToken);
        return Ok(result);
    }
}
