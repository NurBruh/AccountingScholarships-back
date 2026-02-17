using AccountingScholarships.API.Contracts.Requests;

using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Application.Commands.Auth;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new LoginCommand(dto), cancellationToken);

        if (result is null)
            return Unauthorized(new { Message = "Неверное имя пользователя или пароль." });

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RegisterCommand(dto), cancellationToken);
        return CreatedAtAction(nameof(Login), result);
    }
}

