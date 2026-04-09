using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.DTO.EpvoSso.EpvoJoin;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.API.Controllers.Real;

/// <summary>
/// Авторизация через SSO базу (Edu_Users, Edu_Employees, Edu_Positions, Edu_OrgUnits).
/// Тестовый режим: UserId == Password.
/// </summary>
[ApiController]
[Route("api/Auth")]
public class SsoAuthController : ControllerBase
{
    private readonly SsoDbContext _ssoDb;
    private readonly EpvoSsoDbContext _epvoSsoDb;
    private readonly IJwtTokenService _jwtTokenService;

    public SsoAuthController(SsoDbContext ssoDb, EpvoSsoDbContext epvoSsoDb, IJwtTokenService jwtTokenService)
    {
        _ssoDb = ssoDb;
        _epvoSsoDb = epvoSsoDb;
        _jwtTokenService = jwtTokenService;
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
        // Тестовый режим: UserId == Password
        if (string.IsNullOrWhiteSpace(dto.UserId) || string.IsNullOrWhiteSpace(dto.Password))
            return Unauthorized(new { Message = "Введите ID и пароль." });

        if (!int.TryParse(dto.UserId, out var userId))
            return Unauthorized(new { Message = "Некорректный ID пользователя." });

        if (dto.UserId != dto.Password)
            return Unauthorized(new { Message = "Неверный пароль." });

        // Ищем пользователя в SSO по UserID (единый поиск для всех ролей)
        var user = await _ssoDb.Edu_Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.ID == userId, ct);

        if (user is null)
            return Unauthorized(new { Message = "Пользователь не найден." });

        var fullName = $"{user.LastName} {user.FirstName ?? ""} {user.MiddleName ?? ""}".Trim();

        // Получаем должности сотрудника + проверку эдвайзера одним запросом
        var employee = await _ssoDb.Edu_Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.ID == userId, ct);

        var positions = await _ssoDb.Edu_EmployeePositions
            .AsNoTracking()
            .Include(ep => ep.Position)
            .Include(ep => ep.OrgUnit)
            .Where(ep => ep.EmployeeID == userId
                         && ep.Position != null && !ep.Position.Deleted
                         && ep.OrgUnit != null && !ep.OrgUnit.Deleted)
            .ToListAsync(ct);

        // Определяем роль по приоритету:
        // 1. Офис регистратора (полный доступ)
        var registrarPosition = positions.FirstOrDefault(ep =>
            ep.OrgUnit!.Title.Contains("Офис регистратора"));

        if (registrarPosition is not null)
        {
            var token = _jwtTokenService.GenerateToken(
                userId.ToString(), fullName, "registrar");

            return Ok(new SsoAuthResponseDto
            {
                Token = token,
                UserId = userId,
                FullName = fullName,
                Role = "registrar",
                RoleDisplayName = "Офис регистратора",
                ExpiresAt = DateTime.UtcNow.AddHours(8)
            });
        }

        // 2. Директор Института
        var directorPosition = positions.FirstOrDefault(ep =>
            ep.Position!.Title.Contains("Директор", StringComparison.OrdinalIgnoreCase) &&
            ep.Position!.Title.Contains("институт", StringComparison.OrdinalIgnoreCase));

        if (directorPosition is not null)
        {
            var orgUnit = directorPosition.OrgUnit;
            var token = _jwtTokenService.GenerateToken(
                userId.ToString(), fullName, "institute_director",
                "institute", orgUnit?.ID);

            return Ok(new SsoAuthResponseDto
            {
                Token = token,
                UserId = userId,
                FullName = fullName,
                Role = "institute_director",
                RoleDisplayName = "Директор института",
                ExpiresAt = DateTime.UtcNow.AddHours(8),
                ScopeId = orgUnit?.ID,
                ScopeName = orgUnit?.Title
            });
        }

        // 3. Эдвайзер (read-only, свои студенты)
        if (employee is not null && employee.IsAdvisor)
        {
            var token = _jwtTokenService.GenerateToken(
                userId.ToString(), fullName, "advisor");

            return Ok(new SsoAuthResponseDto
            {
                Token = token,
                UserId = userId,
                FullName = fullName,
                Role = "advisor",
                RoleDisplayName = "Эдвайзер",
                ExpiresAt = DateTime.UtcNow.AddHours(8)
            });
        }

        return Unauthorized(new
        {
            Message = "У вас нет доступа к системе. Ваша должность не соответствует ни одной из ролей.",
            Positions = positions.Select(ep => new
            {
                Position = ep.Position?.Title,
                OrgUnit = ep.OrgUnit?.Title
            }),
            IsAdvisor = employee?.IsAdvisor ?? false
        });
    }

    /// <summary>
    /// Получить студентов эдвайзера по его ID
    /// </summary>
    [HttpGet("advisor/{advisorId:int}/students")]
    public async Task<IActionResult> GetAdvisorStudents(int advisorId, CancellationToken ct)
    {
        // Проверяем что это реально эдвайзер
        var employee = await _ssoDb.Edu_Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.ID == advisorId && e.IsAdvisor, ct);

        if (employee is null)
            return NotFound(new { Message = "Эдвайзер не найден." });

        // Получаем студентов эдвайзера через raw SQL по аналогии с SsoStudentDetailsRepository
        // Джоиним Edu_Students → Edu_Users для получения списка,
        // а затем ищем этих студентов в EPVO STUDENT_SSO через IIN
        var advisorStudents = await _ssoDb.Edu_Students
            .AsNoTracking()
            .Include(s => s.User)
            .Include(s => s.EducationPaymentType)
            .Include(s => s.GrantType)
            .Include(s => s.StudyLanguage)
            .Include(s => s.Speciality)
            .Where(s => s.AdvisorID == advisorId)
            .ToListAsync(ct);

        if (advisorStudents.Count == 0)
            return Ok(Array.Empty<object>());

        // Получаем ИИН-ы студентов эдвайзера
        var studentIins = advisorStudents
            .Where(s => s.User?.IIN != null)
            .Select(s => s.User!.IIN!)
            .ToList();

        // Подтягиваем данные из EPVO (STUDENT_SSO) по ИИН
        var epvoStudents = await GetEpvoStudentsByIinsAsync(studentIins, ct);
        var epvoByIin = epvoStudents.ToDictionary(x => x.IinPlt ?? "", x => x);

        // Формируем результат: если есть данные в EPVO — берём оттуда, иначе из SSO
        var result = advisorStudents.Select(s =>
        {
            var iin = s.User?.IIN ?? "";
            if (epvoByIin.TryGetValue(iin, out var epvo))
                return epvo;

            // Если в EPVO нет, собираем из SSO
            return new StudentSsoDetailDto
            {
                StudentId = s.StudentID,
                FullName = s.User?.FullName ?? "",
                IinPlt = iin,
                CourseNumber = s.Year,
                StudyForm = null,
                PaymentType = s.EducationPaymentType?.Title,
                Gpa = s.GPA.HasValue ? (decimal)s.GPA.Value : null,
                StudyLanguage = s.StudyLanguage?.Title,
                ProfessionName = null,
                Specialization = s.Speciality?.Title,
                FacultyName = null,
                Sex = s.User?.Male == true ? "Мужского пола" : s.User?.Male == false ? "Женского пола" : null,
                GrantType = s.GrantType?.Title,
                Iic = null,
            };
        }).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Получить студентов директора института (по факультету / оргюниту)
    /// </summary>
    [HttpGet("director/{userId:int}/students")]
    public async Task<IActionResult> GetDirectorStudents(int userId, CancellationToken ct)
    {
        // Находим институт (OrgUnit) директора
        var directorPosition = await _ssoDb.Edu_EmployeePositions
            .AsNoTracking()
            .Include(ep => ep.Position)
            .Include(ep => ep.OrgUnit)
            .Where(ep => ep.EmployeeID == userId &&
                         ep.Position != null && !ep.Position.Deleted &&
                         ep.Position.Title == "Директор Института")
            .FirstOrDefaultAsync(ct);

        if (directorPosition is null)
            return NotFound(new { Message = "Директор института не найден." });

        var instituteName = directorPosition.OrgUnit?.Title ?? "";

        // Получаем всех студентов из EPVO, фильтруем по факультету директора
        var allStudents = await GetAllEpvoStudentsAsync(ct);

        // Фильтруем студентов, чей факультет содержит название института директора
        var result = allStudents
            .Where(s => !string.IsNullOrEmpty(s.FacultyName) &&
                        s.FacultyName.Contains(instituteName, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Получить студентов из EPVO STUDENT таблицы по набору ИИН
    /// </summary>
    private async Task<List<StudentSsoDetailDto>> GetEpvoStudentsByIinsAsync(List<string> iins, CancellationToken ct)
    {
        if (iins.Count == 0) return new List<StudentSsoDetailDto>();

        var allStudents = await GetAllEpvoStudentsAsync(ct);
        var iinSet = new HashSet<string>(iins);
        return allStudents.Where(s => s.IinPlt != null && iinSet.Contains(s.IinPlt)).ToList();
    }

    private async Task<List<StudentSsoDetailDto>> GetAllEpvoStudentsAsync(CancellationToken ct)
    {
        return await _epvoSsoDb.Database
            .SqlQueryRaw<StudentSsoDetailDto>("""
                SELECT DISTINCT
                    ss.universityId,
                    ss.studentId,
                    CONCAT(ss.lastName, ' ', ss.firstName, ' ', ss.patronymic) AS FullName,
                    ss.iinPlt,
                    ss.courseNumber,
                    sf.nameRu   AS StudyForm,
                    CASE
                        WHEN ss.paymentFormId = 1 THEN N'Платник'
                        WHEN ss.paymentFormId = 2 THEN N'Стипендия'
                    END AS PaymentType,
                    ss.gpa,
                    sl.nameRu   AS StudyLanguage,
                    pe.professionNameRu AS ProfessionName,
                    se.nameRu   AS Specialization,
                    fac.facultyNameRu   AS FacultyName,
                    CASE
                        WHEN ss.sexId = 2 THEN N'Мужского пола'
                        WHEN ss.sexId = 1 THEN N'Женского пола'
                    END AS Sex,
                    CASE
                        WHEN ss.grantType = -4 THEN N'Государственный грант'
                        WHEN ss.grantType = -7 THEN N'Из собственных средств'
                        WHEN ss.grantType = -6 THEN N'Трехсторонняя форма обучения'
                    END AS GrantType,
                    si.iic,
                    si.updateDate
                FROM STUDENT_SSO ss
                LEFT JOIN STUDY_FORMS      sf  ON sf.id          = ss.studyFormId
                LEFT JOIN STUDYLANGUAGES   sl  ON sl.id          = ss.studyLanguageId
                LEFT JOIN PROFESSION       pe  ON pe.professionId = ss.professionid
                LEFT JOIN FACULTIES        fac ON fac.facultyId   = ss.facultyId
                LEFT JOIN SPECIALITIES_EPVO se ON se.id           = ss.specializationId
                LEFT JOIN STUDENT_INFO     si  ON si.studentId    = ss.studentId
            """)
            .ToListAsync(ct);
    }
}
