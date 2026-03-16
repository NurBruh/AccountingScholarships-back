using AccountingScholarships.Application.Queries.EpvoSso;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers;

/// <summary>
/// Данные из EPVO SSO базы (MSSQL). Только чтение.
/// </summary>
[ApiController]
[Route("api/epvo-sso")]
public class EpvoSsoController : ControllerBase
{
    private readonly IMediator _mediator;

    public EpvoSsoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // ─── Professions ──────────────────────────────────────────────

    [HttpGet("professions")]
    public async Task<IActionResult> GetProfessions(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllProfessionsQuery(), ct));

    [HttpGet("professions/{id:int}")]
    public async Task<IActionResult> GetProfession(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetProfessionByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Students ─────────────────────────────────────────────────

    [HttpGet("students")]
    public async Task<IActionResult> GetStudents(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEpvoSsoStudentsQuery(), ct));

    [HttpGet("students/{id:int}")]
    public async Task<IActionResult> GetStudent(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEpvoSsoStudentByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Student Info ─────────────────────────────────────────────

    [HttpGet("students-info")]
    public async Task<IActionResult> GetStudentInfos(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllStudentInfoQuery(), ct));

    [HttpGet("students-info/{id:int}")]
    public async Task<IActionResult> GetStudentInfo(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetStudentInfoByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Scholarships ─────────────────────────────────────────────

    [HttpGet("scholarships")]
    public async Task<IActionResult> GetScholarships(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEpvoSsoScholarshipsQuery(), ct));

    [HttpGet("scholarships/{id:int}")]
    public async Task<IActionResult> GetScholarship(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEpvoSsoScholarshipByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Scholarships New ─────────────────────────────────────────

    [HttpGet("scholarships-new")]
    public async Task<IActionResult> GetScholarshipsNew(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllScholarshipsNewQuery(), ct));

    [HttpGet("scholarships-new/{id:int}")]
    public async Task<IActionResult> GetScholarshipNew(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetScholarshipNewByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Universities ─────────────────────────────────────────────

    [HttpGet("universities")]
    public async Task<IActionResult> GetUniversities(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllUniversitiesQuery(), ct));

    [HttpGet("universities/{id:int}")]
    public async Task<IActionResult> GetUniversity(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetUniversityByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Specialities (EPVO) ──────────────────────────────────────

    [HttpGet("specialities")]
    public async Task<IActionResult> GetSpecialities(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllSpecialitiesQuery(), ct));

    // ─── Specialities New (EPVO) ──────────────────────────────────

    [HttpGet("specialities-new")]
    public async Task<IActionResult> GetSpecialitiesNew(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllSpecialitiesNewQuery(), ct));

    // ─── Specializations ──────────────────────────────────────────

    [HttpGet("specializations")]
    public async Task<IActionResult> GetSpecializations(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllSpecializationsQuery(), ct));

    [HttpGet("specializations/{id:int}")]
    public async Task<IActionResult> GetSpecialization(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetSpecializationByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Study Forms ──────────────────────────────────────────────

    [HttpGet("study-forms")]
    public async Task<IActionResult> GetStudyForms(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllStudyFormsQuery(), ct));

    [HttpGet("study-forms/{id:int}")]
    public async Task<IActionResult> GetStudyForm(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetStudyFormByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Study Calendars ──────────────────────────────────────────

    [HttpGet("study-calendars")]
    public async Task<IActionResult> GetStudyCalendars(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllStudycalendarsQuery(), ct));

    [HttpGet("study-calendars/{id:int}")]
    public async Task<IActionResult> GetStudyCalendar(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetStudycalendarByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }
}
