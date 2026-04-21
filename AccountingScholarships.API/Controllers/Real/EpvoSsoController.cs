using AccountingScholarships.Application.Commands.StoredProcedures;
using AccountingScholarships.Application.Queries.EpvoSso;
using AccountingScholarships.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AccountingScholarships.API.Controllers.Real;

/// <summary>
/// Данные из EPVO SSO базы (MSSQL). Только чтение.
/// </summary>
[ApiController]
[Route("api/epvo-sso")]
public class EpvoSsoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IStoredProcedureRepository _spRepo;

    public EpvoSsoController(IMediator mediator, IStoredProcedureRepository spRepo)
    {
        _mediator = mediator;
        _spRepo = spRepo;
    }

    // ─── Professions ──────────────────────────────────────────────

    [HttpGet("professions")]
    public async Task<IActionResult> GetProfessions(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllProfessionsQuery(), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }
    [HttpGet("professions/{id:int}")]
    public async Task<IActionResult> GetProfession(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetProfessionByIdQuery(id), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }
    [HttpGet("profession-2025")]
    public async Task<IActionResult> GetProfessionNew(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEpvoProfession2025Query(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Students ─────────────────────────────────────────────────

    [HttpGet("students")]
    public async Task<IActionResult> GetStudents(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEpvoSsoStudentsQuery(), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("students/{id:int}")]
    public async Task<IActionResult> GetStudent(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEpvoSsoStudentByIdQuery(id), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Student Info ─────────────────────────────────────────────

    [HttpGet("students-info")]
    public async Task<IActionResult> GetStudentInfos(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllStudentInfoQuery(), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("students-info/{id:int}")]
    public async Task<IActionResult> GetStudentInfo(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetStudentInfoByIdQuery(id), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Scholarships ─────────────────────────────────────────────

    [HttpGet("scholarships")]
    public async Task<IActionResult> GetScholarships(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEpvoSsoScholarshipsQuery(), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("scholarships/{id:int}")]
    public async Task<IActionResult> GetScholarship(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEpvoSsoScholarshipByIdQuery(id), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Scholarships New ─────────────────────────────────────────

    [HttpGet("scholarships-new")]
    public async Task<IActionResult> GetScholarshipsNew(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllScholarshipsNewQuery(), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("scholarships-new/{id:int}")]
    public async Task<IActionResult> GetScholarshipNew(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetScholarshipNewByIdQuery(id), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Universities ─────────────────────────────────────────────

    [HttpGet("universities")]
    public async Task<IActionResult> GetUniversities(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllUniversitiesQuery(), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("universities/{id:int}")]
    public async Task<IActionResult> GetUniversity(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetUniversityByIdQuery(id), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Specialities (EPVO) ──────────────────────────────────────

    [HttpGet("specialities")]
    public async Task<IActionResult> GetSpecialities(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllSpecialitiesQuery(), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Specialities New (EPVO) ──────────────────────────────────

    [HttpGet("specialities-new")]
    public async Task<IActionResult> GetSpecialitiesNew(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllSpecialitiesNewQuery(), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }    

    // ─── Specializations ──────────────────────────────────────────

    [HttpGet("specializations")]
    public async Task<IActionResult> GetSpecializations(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllSpecializationsQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
        

    [HttpGet("specializations/{id:int}")]
    public async Task<IActionResult> GetSpecialization(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetSpecializationByIdQuery(id), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Study Forms ──────────────────────────────────────────────

    [HttpGet("study-forms")]
    public async Task<IActionResult> GetStudyForms(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllStudyFormsQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
        

    [HttpGet("study-forms/{id:int}")]
    public async Task<IActionResult> GetStudyForm(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetStudyFormByIdQuery(id), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Study Calendars ──────────────────────────────────────────

    [HttpGet("study-calendars")]
    public async Task<IActionResult> GetStudyCalendars(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllStudycalendarsQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
        

    [HttpGet("study-calendars/{id:int}")]
    public async Task<IActionResult> GetStudyCalendar(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetStudycalendarByIdQuery(id), ct);
        if(result is null)
            return NotFound();
        return Ok(result);
    }
    [HttpGet("faculties")]
    public async Task<IActionResult> GetFaculties(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllFacultiesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("faculties/{id:int}")]
    public async Task<IActionResult> GetFaculty(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetFacultyByIdQuery(id), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Center KATO ──────────────────────────────────────────────

    [HttpGet("center-kato")]
    public async Task<IActionResult> GetCenterKato(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllCenterKatoQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("center-kato/{id:int}")]
    public async Task<IActionResult> GetCenterKatoById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetCenterKatoByIdQuery(id), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Center Countries ─────────────────────────────────────────

    [HttpGet("center-countries")]
    public async Task<IActionResult> GetCenterCountries(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllCenterCountriesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("center-countries/{id:int}")]
    public async Task<IActionResult> GetCenterCountry(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetCenterCountriesByIdQuery(id), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Center Nationalities ─────────────────────────────────────

    [HttpGet("center-nationalities")]
    public async Task<IActionResult> GetCenterNationalities(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllCenterNationalitiesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("center-nationalities/{id:int}")]
    public async Task<IActionResult> GetCenterNationality(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetCenterNationalitiesByIdQuery(id), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Marital States ───────────────────────────────────────────

    [HttpGet("maritalstates")]
    public async Task<IActionResult> GetMaritalstates(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllMaritalstatesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("maritalstates/{id:int}")]
    public async Task<IActionResult> GetMaritalstate(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetMaritalstateByIdQuery(id), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Nationalities ────────────────────────────────────────────

    [HttpGet("nationalities")]
    public async Task<IActionResult> GetNationalities(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllNationalitiesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("nationalities/{id:int}")]
    public async Task<IActionResult> GetNationality(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetNationalityByIdQuery(id), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Study Languages ──────────────────────────────────────────

    [HttpGet("study-languages")]
    public async Task<IActionResult> GetStudyLanguages(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllStudyLanguagesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("study-languages/{id:int}")]
    public async Task<IActionResult> GetStudyLanguage(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetStudyLanguageByIdQuery(id), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Students Temp ────────────────────────────────────────────

    [HttpGet("students-temp")]
    public async Task<IActionResult> GetStudentTemps(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllStudentTempsQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("students-temp/{id:int}")]
    public async Task<IActionResult> GetStudentTemp(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetStudentTempByIdQuery(id), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
    // ─── Students Temp
    [HttpGet("students-dump")]
    public async Task<IActionResult> GetStudentDump(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEpvoStudentDumpQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Students SSO ─────────────────────────────────────────────

    [HttpGet("students-sso")]
    public async Task<IActionResult> GetStudentsSso(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllStudentSsoQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Student Change Logs ──────────────────────────────────────

    [HttpGet("student-change-logs")]
    public async Task<IActionResult> GetStudentChangeLogs(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllStudentChangeLogsQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Stored Procedures ───────────────────────────────────────

    /// <summary>
    /// Предпросмотр синхронизации: выполняет [dbo].[Reload_STUDENT] (read-only),
    /// проверяет дубликаты по iinPlt против STUDENT_SSO и STUDENT.
    /// Возвращает список с флагом IsDuplicate и статистику.
    /// </summary>
    [HttpGet("sync-preview")]
    public async Task<IActionResult> GetSyncPreview(CancellationToken ct)
    {
        var result = await _spRepo.GetSyncPreviewAsync(ct);
        return Ok(result);
    }

    /// <summary>
    /// Выполняет [dbo].[Reload_STUDENT] и возвращает SELECT-результат (как в SSMS).
    /// Только чтение — ничего не записывает.
    /// </summary>
    [HttpGet("reload-student-preview")]
    public async Task<IActionResult> ReloadStudentPreview(CancellationToken ct)
    {
        var students = await _spRepo.ReadReloadStudentAsync(ct);
        return Ok(new
        {
            Count = students.Count,
            Students = students
        });
    }

    /// <summary>
    /// Выполняет [dbo].[Reload_STUDENT] и сохраняет результат в STUDENT_TEMP.
    /// STUDENT_TEMP очищается перед вставкой.
    /// </summary>
    [HttpPost("save-to-temp")]
    public async Task<IActionResult> SaveToTemp(CancellationToken ct)
    {
        var count = await _spRepo.SaveReloadStudentToTempAsync(ct);
        return Ok(new
        {
            Message = $"STUDENT_TEMP обновлён. Загружено записей: {count}",
            Count = count
        });
    }

    /// <summary>
    /// Читает STUDENT_TEMP, симулирует отправку в ЕПВО,
    /// каждую попытку пишет в STUDENT_SYNC_LOG (Success / Error).
    /// STUDENT_SSO не затрагивается. Фиксирует кто запустил.
    /// </summary>
    [HttpPost("send-temp-to-epvo")]
    public async Task<IActionResult> SendTempToEpvo(CancellationToken ct)
    {
        var triggeredBy = User.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value
                       ?? User.FindFirst(ClaimTypes.Name)?.Value
                       ?? "Неизвестно";
        var result = await _spRepo.SendTempToEpvoAsync(triggeredBy, ct);
        return Ok(result);
    }

    // ─── Sync Logs ────────────────────────────────────────────────

    /// <summary>
    /// Возвращает историю отправок в ЕПВО.
    /// Опционально фильтрует по статусу: Pending / Success / Error
    /// </summary>
    [HttpGet("sync-logs")]
    public async Task<IActionResult> GetSyncLogs([FromQuery] string? status, [FromQuery] int page = 1, [FromQuery] int pageSize = 50, CancellationToken ct = default)
    {
        var result = await _spRepo.GetSyncLogsAsync(status, page, pageSize, ct);
        return Ok(result);
    }

    /// <summary>
    /// Логи по конкретному студенту (studentId или iin)
    /// </summary>
    [HttpGet("sync-logs/student/{studentId:int}")]
    public async Task<IActionResult> GetSyncLogsByStudent(int studentId, CancellationToken ct)
    {
        var logs = await _spRepo.GetSyncLogsByStudentAsync(studentId, ct);
        return Ok(logs);
    }

    
}
