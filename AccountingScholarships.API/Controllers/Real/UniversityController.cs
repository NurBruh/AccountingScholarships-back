using AccountingScholarships.Application.Queries.University.Academic;
using AccountingScholarships.Application.Queries.University.Organization;
using AccountingScholarships.Application.Queries.University.ReferenceData;
using AccountingScholarships.Application.Queries.University.Students;
using AccountingScholarships.Application.Queries.University.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingScholarships.API.Controllers.Real;

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

    // ─── Academic Statuses ────────────────────────────────────────
    [HttpGet("academic-statuses")]
    public async Task<IActionResult> GetAcademicStatuses(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduAcademicStatusesQuery(), ct));

    [HttpGet("academic-statuses/{id:int}")]
    public async Task<IActionResult> GetAcademicStatusById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduAcademicStatusByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Address Types ────────────────────────────────────────────
    [HttpGet("address-types")]
    public async Task<IActionResult> GetAddressTypes(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduAddressTypesQuery(), ct));

    [HttpGet("address-types/{id:int}")]
    public async Task<IActionResult> GetAddressTypeById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduAddressTypeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Citizen Categories ───────────────────────────────────────
    [HttpGet("citizen-categories")]
    public async Task<IActionResult> GetCitizenCategories(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduCitizenCategoriesQuery(), ct));

    [HttpGet("citizen-categories/{id:int}")]
    public async Task<IActionResult> GetCitizenCategoryById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduCitizenCategoryByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Control Types ────────────────────────────────────────────
    [HttpGet("control-types")]
    public async Task<IActionResult> GetControlTypes(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduControlTypesQuery(), ct));

    [HttpGet("control-types/{id:int}")]
    public async Task<IActionResult> GetControlTypeById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduControlTypeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Countries ────────────────────────────────────────────────
    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduCountriesQuery(), ct));

    [HttpGet("countries/{id:int}")]
    public async Task<IActionResult> GetCountryById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduCountryByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Course Types ─────────────────────────────────────────────
    [HttpGet("course-types")]
    public async Task<IActionResult> GetCourseTypes(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduCourseTypesQuery(), ct));

    [HttpGet("course-types/{id:int}")]
    public async Task<IActionResult> GetCourseTypeById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduCourseTypeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Course Type DVO ──────────────────────────────────────────
    [HttpGet("course-type-dvo")]
    public async Task<IActionResult> GetCourseTypeDvo(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduCourseTypeDvoQuery(), ct));

    [HttpGet("course-type-dvo/{id:int}")]
    public async Task<IActionResult> GetCourseTypeDvoById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduCourseTypeDvoByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Document Issue Orgs ──────────────────────────────────────
    [HttpGet("document-issue-orgs")]
    public async Task<IActionResult> GetDocumentIssueOrgs(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduDocumentIssueOrgsQuery(), ct));

    [HttpGet("document-issue-orgs/{id:int}")]
    public async Task<IActionResult> GetDocumentIssueOrgById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduDocumentIssueOrgByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Education Document SubTypes ──────────────────────────────
    [HttpGet("education-document-subtypes")]
    public async Task<IActionResult> GetEducationDocumentSubTypes(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduEducationDocumentSubTypesQuery(), ct));

    [HttpGet("education-document-subtypes/{id:int}")]
    public async Task<IActionResult> GetEducationDocumentSubTypeById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduEducationDocumentSubTypeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Education Document Types ─────────────────────────────────
    [HttpGet("education-document-types")]
    public async Task<IActionResult> GetEducationDocumentTypes(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduEducationDocumentTypesQuery(), ct));

    [HttpGet("education-document-types/{id:int}")]
    public async Task<IActionResult> GetEducationDocumentTypeById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduEducationDocumentTypeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Education Durations ──────────────────────────────────────
    [HttpGet("education-durations")]
    public async Task<IActionResult> GetEducationDurations(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduEducationDurationsQuery(), ct));

    [HttpGet("education-durations/{id:int}")]
    public async Task<IActionResult> GetEducationDurationById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduEducationDurationByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Education Payment Types ──────────────────────────────────
    [HttpGet("education-payment-types")]
    public async Task<IActionResult> GetEducationPaymentTypes(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduEducationPaymentTypesQuery(), ct));

    [HttpGet("education-payment-types/{id:int}")]
    public async Task<IActionResult> GetEducationPaymentTypeById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduEducationPaymentTypeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Education Types ──────────────────────────────────────────
    [HttpGet("education-types")]
    public async Task<IActionResult> GetEducationTypes(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduEducationTypesQuery(), ct));

    [HttpGet("education-types/{id:int}")]
    public async Task<IActionResult> GetEducationTypeById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduEducationTypeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Employee Positions ───────────────────────────────────────
    [HttpGet("employee-positions")]
    public async Task<IActionResult> GetEmployeePositions(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduEmployeePositionsQuery(), ct));

    [HttpGet("employee-positions/{id:int}")]
    public async Task<IActionResult> GetEmployeePositionById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduEmployeePositionByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Employees ────────────────────────────────────────────────
    [HttpGet("employees")]
    public async Task<IActionResult> GetEmployees(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduEmployeesQuery(), ct));

    [HttpGet("employees/{id:int}")]
    public async Task<IActionResult> GetEmployeeById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduEmployeeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Entrant Statuses ─────────────────────────────────────────
    [HttpGet("entrant-statuses")]
    public async Task<IActionResult> GetEntrantStatuses(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduEntrantStatusesQuery(), ct));

    [HttpGet("entrant-statuses/{id:int}")]
    public async Task<IActionResult> GetEntrantStatusById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduEntrantStatusByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Entrants ─────────────────────────────────────────────────
    [HttpGet("entrants")]
    public async Task<IActionResult> GetEntrants(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduEntrantsQuery(), ct));

    [HttpGet("entrants/{id:int}")]
    public async Task<IActionResult> GetEntrantById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduEntrantByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Grant Types ──────────────────────────────────────────────
    [HttpGet("grant-types")]
    public async Task<IActionResult> GetGrantTypes(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduGrantTypesQuery(), ct));

    [HttpGet("grant-types/{id:int}")]
    public async Task<IActionResult> GetGrantTypeById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduGrantTypeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Languages ────────────────────────────────────────────────
    [HttpGet("languages")]
    public async Task<IActionResult> GetLanguages(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduLanguagesQuery(), ct));

    [HttpGet("languages/{id:int}")]
    public async Task<IActionResult> GetLanguageById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduLanguageByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Localities ───────────────────────────────────────────────
    [HttpGet("localities")]
    public async Task<IActionResult> GetLocalities(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduLocalitiesQuery(), ct));

    [HttpGet("localities/{id:int}")]
    public async Task<IActionResult> GetLocalityById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduLocalityByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Locality Types ───────────────────────────────────────────
    [HttpGet("locality-types")]
    public async Task<IActionResult> GetLocalityTypes(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduLocalityTypesQuery(), ct));

    [HttpGet("locality-types/{id:int}")]
    public async Task<IActionResult> GetLocalityTypeById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduLocalityTypeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Marital Statuses ─────────────────────────────────────────
    [HttpGet("marital-statuses")]
    public async Task<IActionResult> GetMaritalStatuses(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduMaritalStatusesQuery(), ct));

    [HttpGet("marital-statuses/{id:int}")]
    public async Task<IActionResult> GetMaritalStatusById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduMaritalStatusByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Messengers ───────────────────────────────────────────────
    [HttpGet("messengers")]
    public async Task<IActionResult> GetMessengers(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduMessengersQuery(), ct));

    [HttpGet("messengers/{id:int}")]
    public async Task<IActionResult> GetMessengerById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduMessengerByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Nationalities ────────────────────────────────────────────
    [HttpGet("nationalities")]
    public async Task<IActionResult> GetNationalities(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduNationalitiesQuery(), ct));

    [HttpGet("nationalities/{id:int}")]
    public async Task<IActionResult> GetNationalityById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduNationalityByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Org Units ────────────────────────────────────────────────
    [HttpGet("org-units")]
    public async Task<IActionResult> GetOrgUnits(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduOrgUnitsQuery(), ct));

    [HttpGet("org-units/{id:int}")]
    public async Task<IActionResult> GetOrgUnitById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduOrgUnitByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Org Unit Types ───────────────────────────────────────────
    [HttpGet("org-unit-types")]
    public async Task<IActionResult> GetOrgUnitTypes(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduOrgUnitTypesQuery(), ct));

    [HttpGet("org-unit-types/{id:int}")]
    public async Task<IActionResult> GetOrgUnitTypeById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduOrgUnitTypeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Position Categories ──────────────────────────────────────
    [HttpGet("position-categories")]
    public async Task<IActionResult> GetPositionCategories(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduPositionCategoriesQuery(), ct));

    [HttpGet("position-categories/{id:int}")]
    public async Task<IActionResult> GetPositionCategoryById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduPositionCategoryByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Positions ────────────────────────────────────────────────
    [HttpGet("positions")]
    public async Task<IActionResult> GetPositions(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduPositionsQuery(), ct));

    [HttpGet("positions/{id:int}")]
    public async Task<IActionResult> GetPositionById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduPositionByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Regions (Obsolete) ───────────────────────────────────────
    [HttpGet("regions")]
    public async Task<IActionResult> GetRegions(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllObsoleteEduRegionsQuery(), ct));

    // ─── RUP Algorithms ───────────────────────────────────────────
    [HttpGet("rup-algorithms")]
    public async Task<IActionResult> GetRupAlgorithms(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduRupAlgorithmsQuery(), ct));

    [HttpGet("rup-algorithms/{id:int}")]
    public async Task<IActionResult> GetRupAlgorithmById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduRupAlgorithmByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── RUPs ─────────────────────────────────────────────────────
    [HttpGet("rups")]
    public async Task<IActionResult> GetRups(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduRupsQuery(), ct));

    [HttpGet("rups/{id:int}")]
    public async Task<IActionResult> GetRupById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduRupByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── School Region Statuses ───────────────────────────────────
    [HttpGet("school-region-statuses")]
    public async Task<IActionResult> GetSchoolRegionStatuses(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduSchoolRegionStatusesQuery(), ct));

    [HttpGet("school-region-statuses/{id:int}")]
    public async Task<IActionResult> GetSchoolRegionStatusById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduSchoolRegionStatusByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Schools ──────────────────────────────────────────────────
    [HttpGet("schools")]
    public async Task<IActionResult> GetSchools(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduSchoolsQuery(), ct));

    [HttpGet("schools/{id:int}")]
    public async Task<IActionResult> GetSchoolById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduSchoolByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── School Subjects ──────────────────────────────────────────
    [HttpGet("school-subjects")]
    public async Task<IActionResult> GetSchoolSubjects(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduSchoolSubjectsQuery(), ct));

    [HttpGet("school-subjects/{id:int}")]
    public async Task<IActionResult> GetSchoolSubjectById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduSchoolSubjectByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── School Types ─────────────────────────────────────────────
    [HttpGet("school-types")]
    public async Task<IActionResult> GetSchoolTypes(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduSchoolTypesQuery(), ct));

    [HttpGet("school-types/{id:int}")]
    public async Task<IActionResult> GetSchoolTypeById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduSchoolTypeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Semester Courses ─────────────────────────────────────────
    [HttpGet("semester-courses")]
    public async Task<IActionResult> GetSemesterCourses(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduSemesterCoursesQuery(), ct));

    [HttpGet("semester-courses/{id:int}")]
    public async Task<IActionResult> GetSemesterCourseById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduSemesterCourseByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Semesters ────────────────────────────────────────────────
    [HttpGet("semesters")]
    public async Task<IActionResult> GetSemesters(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduSemestersQuery(), ct));

    [HttpGet("semesters/{id:int}")]
    public async Task<IActionResult> GetSemesterById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduSemesterByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Semester Types ───────────────────────────────────────────
    [HttpGet("semester-types")]
    public async Task<IActionResult> GetSemesterTypes(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduSemesterTypesQuery(), ct));

    [HttpGet("semester-types/{id:int}")]
    public async Task<IActionResult> GetSemesterTypeById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduSemesterTypeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Specialities ─────────────────────────────────────────────
    [HttpGet("specialities")]
    public async Task<IActionResult> GetSpecialities(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduSpecialitiesQuery(), ct));

    [HttpGet("specialities/{id:int}")]
    public async Task<IActionResult> GetSpecialityById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduSpecialityByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Speciality Levels ────────────────────────────────────────
    [HttpGet("speciality-levels")]
    public async Task<IActionResult> GetSpecialityLevels(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduSpecialityLevelsQuery(), ct));

    [HttpGet("speciality-levels/{id:int}")]
    public async Task<IActionResult> GetSpecialityLevelById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduSpecialityLevelByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Speciality Specializations ───────────────────────────────
    [HttpGet("speciality-specializations")]
    public async Task<IActionResult> GetSpecialitySpecializations(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduSpecialitySpecializationsQuery(), ct));

    [HttpGet("speciality-specializations/{id:int}")]
    public async Task<IActionResult> GetSpecialitySpecializationById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduSpecialitySpecializationByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Specializations ──────────────────────────────────────────
    [HttpGet("specializations")]
    public async Task<IActionResult> GetSpecializations(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduSpecializationsQuery(), ct));

    [HttpGet("specializations/{id:int}")]
    public async Task<IActionResult> GetSpecializationById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduSpecializationByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Specializations-OrgUnits ─────────────────────────────────
    [HttpGet("specializations-org-units")]
    public async Task<IActionResult> GetSpecializationsOrgUnits(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduSpecializationsOrgUnitsQuery(), ct));

    // ─── Student Categories ───────────────────────────────────────
    [HttpGet("student-categories")]
    public async Task<IActionResult> GetStudentCategories(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduStudentCategoriesQuery(), ct));

    [HttpGet("student-categories/{id:int}")]
    public async Task<IActionResult> GetStudentCategoryById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduStudentCategoryByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Student Courses ──────────────────────────────────────────
    [HttpGet("student-courses")]
    public async Task<IActionResult> GetStudentCourses(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduStudentCoursesQuery(), ct));

    [HttpGet("student-courses/{id:int}")]
    public async Task<IActionResult> GetStudentCourseById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduStudentCourseByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Student Statuses ─────────────────────────────────────────
    [HttpGet("student-statuses")]
    public async Task<IActionResult> GetStudentStatuses(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduStudentStatusesQuery(), ct));

    [HttpGet("student-statuses/{id:int}")]
    public async Task<IActionResult> GetStudentStatusById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduStudentStatusByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Students ─────────────────────────────────────────────────
    [HttpGet("students")]
    public async Task<IActionResult> GetStudents(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllSsoStudentsQuery(), ct));

    // ─── Student Info Translations ────────────────────────────────
    [HttpGet("student-info-translations")]
    public async Task<IActionResult> GetStudentInfoTranslations(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllStudentInfoTranslationsQuery(), ct));

    // ─── User Addresses ───────────────────────────────────────────
    [HttpGet("user-addresses")]
    public async Task<IActionResult> GetUserAddresses(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduUserAddressesQuery(), ct));

    [HttpGet("user-addresses/{id:int}")]
    public async Task<IActionResult> GetUserAddressById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduUserAddressByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── User Document Types ──────────────────────────────────────
    [HttpGet("user-document-types")]
    public async Task<IActionResult> GetUserDocumentTypes(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduUserDocumentTypesQuery(), ct));

    [HttpGet("user-document-types/{id:int}")]
    public async Task<IActionResult> GetUserDocumentTypeById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduUserDocumentTypeByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── User Documents ───────────────────────────────────────────
    [HttpGet("user-documents")]
    public async Task<IActionResult> GetUserDocuments(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduUserDocumentsQuery(), ct));

    [HttpGet("user-documents/{id:int}")]
    public async Task<IActionResult> GetUserDocumentById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduUserDocumentByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── User Education ───────────────────────────────────────────
    [HttpGet("user-education")]
    public async Task<IActionResult> GetUserEducation(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduUserEducationQuery(), ct));

    [HttpGet("user-education/{id:int}")]
    public async Task<IActionResult> GetUserEducationById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduUserEducationByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    // ─── Users ────────────────────────────────────────────────────
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers(CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllEduUsersQuery(), ct));

    [HttpGet("users/{id:int}")]
    public async Task<IActionResult> GetUserById(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetEduUserByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }
    [HttpGet("scollarship-students-info")]
    public async Task<IActionResult> GetScollarshipStudentsInfo(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetAllScollarshipStudentsInfo(), ct);
        return result is null ? NotFound() : Ok(result);
    }
}
