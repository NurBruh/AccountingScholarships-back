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

    // ─── Center KATO ──────────────────────────────────────────────

    [HttpGet("center-kato")]
    public async Task<IActionResult> GetCenterKato(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllCenterKatoQuery(), ct);
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

    // ─── Center Nationalities ─────────────────────────────────────

    [HttpGet("center-nationalities")]
    public async Task<IActionResult> GetCenterNationalities(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllCenterNationalitiesQuery(), ct);
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

    // ─── Nationalities ────────────────────────────────────────────

    [HttpGet("nationalities")]
    public async Task<IActionResult> GetNationalities(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllNationalitiesQuery(), ct);
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
}

