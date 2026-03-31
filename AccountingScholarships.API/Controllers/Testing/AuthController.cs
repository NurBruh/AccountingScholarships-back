using AccountingScholarships.API.Contracts.Requests;

using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Application.Commands.Auth;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers.Testing;

/// <summary>
/// Контроллер для управления аутентификацией и регистрацией пользователей.
/// </summary>
[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Авторизация пользователя.
    /// </summary>
    /// <param name="dto">Данные для входа (логин и пароль).</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>JWT токен при успешной авторизации.</returns>
    /// <response code="200">Успешная авторизация, возвращает токен.</response>
    /// <response code="401">Неверное имя пользователя или пароль.</response>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new LoginCommand(dto), cancellationToken);

        if (result is null)
            return Unauthorized(new { Message = "Неверное имя пользователя или пароль." });

        return Ok(result);
    }

    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <param name="dto">Данные для регистрации.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Данные созданного пользователя.</returns>
    /// <response code="201">Пользователь успешно зарегистрирован.</response>
    /// <response code="400">Ошибки валидации или неверные данные.</response>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RegisterCommand(dto), cancellationToken);
        return CreatedAtAction(nameof(Login), result);
    }
}