using AccountingScholarships.Application.Queries.University.ReferenceData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers;

/// <summary>
/// Справочные данные из University базы (SSO). Только чтение.
/// </summary>
[ApiController]
[Route("api/university")]
public class UniversityController : ControllerBase
{
    private readonly IMediator _mediator;

    public UniversityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // ─── Address Types ────────────────────────────────────────────

    [HttpGet("address-types")]
    public async Task<IActionResult> GetAddressTypes(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduAddressTypesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Localities ───────────────────────────────────────────────

    [HttpGet("localities")]
    public async Task<IActionResult> GetLocalities(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduLocalitiesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Locality Types ───────────────────────────────────────────

    [HttpGet("locality-types")]
    public async Task<IActionResult> GetLocalityTypes(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduLocalityTypesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Regions (Obsolete) ───────────────────────────────────────

    [HttpGet("regions")]
    public async Task<IActionResult> GetRegions(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllObsoleteEduRegionsQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Schools ──────────────────────────────────────────────────

    [HttpGet("schools")]
    public async Task<IActionResult> GetSchools(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduSchoolsQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── School Types ─────────────────────────────────────────────

    [HttpGet("school-types")]
    public async Task<IActionResult> GetSchoolTypes(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduSchoolTypesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── School Region Statuses ───────────────────────────────────

    [HttpGet("school-region-statuses")]
    public async Task<IActionResult> GetSchoolRegionStatuses(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduSchoolRegionStatusesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Semesters ────────────────────────────────────────────────

    [HttpGet("semesters")]
    public async Task<IActionResult> GetSemesters(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduSemestersQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Semester Types ───────────────────────────────────────────

    [HttpGet("semester-types")]
    public async Task<IActionResult> GetSemesterTypes(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduSemesterTypesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Control Types ────────────────────────────────────────────

    [HttpGet("control-types")]
    public async Task<IActionResult> GetControlTypes(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduControlTypesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Course Types ─────────────────────────────────────────────

    [HttpGet("course-types")]
    public async Task<IActionResult> GetCourseTypes(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduCourseTypesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Course Type DVO ──────────────────────────────────────────

    [HttpGet("course-type-dvo")]
    public async Task<IActionResult> GetCourseTypeDvo(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduCourseTypeDvoQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Education Document Types ─────────────────────────────────

    [HttpGet("education-document-types")]
    public async Task<IActionResult> GetEducationDocumentTypes(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduEducationDocumentTypesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Education Document SubTypes ──────────────────────────────

    [HttpGet("education-document-subtypes")]
    public async Task<IActionResult> GetEducationDocumentSubTypes(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduEducationDocumentSubTypesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Specializations ──────────────────────────────────────────

    [HttpGet("specializations")]
    public async Task<IActionResult> GetSpecializations(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduSpecializationsQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    // ─── Entrant Statuses ─────────────────────────────────────────

    [HttpGet("entrant-statuses")]
    public async Task<IActionResult> GetEntrantStatuses(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllEduEntrantStatusesQuery(), ct);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
}
