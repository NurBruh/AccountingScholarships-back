using AccountingScholarships.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers;
/// <summary>
/// Данные из EPVO SSO базы (MSSQL). Только чтение.
/// </summary>
[ApiController]
[Route("api/epvo-sso")]
public class EpvoSsoController : ControllerBase
{
    private readonly IEpvoProfessionRepository _professions;
    public EpvoSsoController(
        IEpvoProfessionRepository professions)
    {
        _professions = professions;
    }
    // ─── Professions ──────────────────────────────────────────────
    [HttpGet("professions")]
    public async Task<IActionResult> GetProfessions(CancellationToken ct)
        => Ok(await _professions.GetAllAsDtoAsync(ct));
    [HttpGet("professions/{id:int}")]
    public async Task<IActionResult> GetProfession(int id, CancellationToken ct)
    {
        var result = await _professions.GetAsDtoAsync(id, ct);
        return result is null
            ? NotFound(new { message = $"Profession с ID={id} не найден." })
            : Ok(result);
    }
}