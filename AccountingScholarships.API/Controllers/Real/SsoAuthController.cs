using AccountingScholarships.Application.DTO;
using AccountingScholarships.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers.Real;

/// <summary>
/// Авторизация через SSO базу (Edu_Users, Edu_Employees, Edu_Positions, Edu_OrgUnits).
/// Тестовый режим: UserId == Password.
/// </summary>
[ApiController]
[Route("api/Auth")]
public class SsoAuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public SsoAuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Вход в систему. Определяет роль автоматически:
    /// - advisor (эдвайзер) — IsAdvisor=1
    /// - registrar (офис регистратора) — основная роль
    /// - institute_director (директор института) — только свой институт
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] SsoLoginDto dto, CancellationToken ct)
    {
        var result = await _authService.LoginAsync(dto, ct);
        if (!result.IsSuccess)
            return Unauthorized(new { Message = result.ErrorMessage });
        return Ok(result.Data);
    }

    /// <summary>
    /// Получить студентов эдвайзера по его ID
    /// </summary>
    [HttpGet("advisor/{advisorId:int}/students")]
    public async Task<IActionResult> GetAdvisorStudents(int advisorId, CancellationToken ct)
    {
        var result = await _authService.GetAdvisorStudentsAsync(advisorId, ct);
        if (!result.IsSuccess)
            return result.IsNotFound
                ? NotFound(new { Message = result.ErrorMessage })
                : Unauthorized(new { Message = result.ErrorMessage });
        return Ok(result.Data);
    }

    /// <summary>
    /// Получить студентов директора института (по факультету / оргюниту)
    /// </summary>
    [HttpGet("director/{userId:int}/students")]
    public async Task<IActionResult> GetDirectorStudents(int userId, CancellationToken ct)
    {
        var result = await _authService.GetDirectorStudentsAsync(userId, ct);
        if (!result.IsSuccess)
            return result.IsNotFound
                ? NotFound(new { Message = result.ErrorMessage })
                : Unauthorized(new { Message = result.ErrorMessage });
        return Ok(result.Data);
    }
}

