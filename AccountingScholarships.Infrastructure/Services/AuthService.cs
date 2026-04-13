using AccountingScholarships.Application.Common;
using AccountingScholarships.Application.DTO;
using AccountingScholarships.Application.DTO.EpvoSso.EpvoJoin;
using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly SsoDbContext _ssoDb;
    private readonly EpvoSsoDbContext _epvoSsoDb;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(SsoDbContext ssoDb, EpvoSsoDbContext epvoSsoDb, IJwtTokenService jwtTokenService)
    {
        _ssoDb = ssoDb;
        _epvoSsoDb = epvoSsoDb;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResult<SsoAuthResponseDto>> LoginAsync(SsoLoginDto dto, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(dto.UserId) || string.IsNullOrWhiteSpace(dto.Password))
            return AuthResult<SsoAuthResponseDto>.Unauthorized("Введите ID и пароль.");

        if (!int.TryParse(dto.UserId, out var userId))
            return AuthResult<SsoAuthResponseDto>.Unauthorized("Некорректный ID пользователя.");

        if (dto.UserId != dto.Password)
            return AuthResult<SsoAuthResponseDto>.Unauthorized("Неверный пароль.");

        var user = await _ssoDb.Edu_Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.ID == userId, ct);

        if (user is null)
            return AuthResult<SsoAuthResponseDto>.Unauthorized("Пользователь не найден.");

        var fullName = $"{user.LastName} {user.FirstName ?? ""} {user.MiddleName ?? ""}".Trim();

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

        // 1. Офис регистратора
        var registrarPosition = positions.FirstOrDefault(ep =>
            ep.OrgUnit!.Title.Contains("Офис регистратора"));

        if (registrarPosition is not null)
        {
            var token = _jwtTokenService.GenerateToken(userId.ToString(), fullName, "registrar");
            return AuthResult<SsoAuthResponseDto>.Success(new SsoAuthResponseDto
            {
                Token = token,
                UserId = userId,
                FullName = fullName,
                Role = "registrar",
                RoleDisplayName = "Офис регистратора",
                ExpiresAt = DateTime.UtcNow.AddHours(8)
            });
        }

        // 2. Директор института
        var directorPosition = positions.FirstOrDefault(ep =>
            ep.Position!.Title.Contains("Директор", StringComparison.OrdinalIgnoreCase) &&
            ep.Position!.Title.Contains("институт", StringComparison.OrdinalIgnoreCase));

        if (directorPosition is not null)
        {
            var orgUnit = directorPosition.OrgUnit;
            var token = _jwtTokenService.GenerateToken(
                userId.ToString(), fullName, "institute_director",
                "institute", orgUnit?.ID);

            return AuthResult<SsoAuthResponseDto>.Success(new SsoAuthResponseDto
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

        // 3. Эдвайзер
        if (employee is not null && employee.IsAdvisor)
        {
            var token = _jwtTokenService.GenerateToken(userId.ToString(), fullName, "advisor");
            return AuthResult<SsoAuthResponseDto>.Success(new SsoAuthResponseDto
            {
                Token = token,
                UserId = userId,
                FullName = fullName,
                Role = "advisor",
                RoleDisplayName = "Эдвайзер",
                ExpiresAt = DateTime.UtcNow.AddHours(8)
            });
        }

        return AuthResult<SsoAuthResponseDto>.Unauthorized(
            $"У вас нет доступа к системе. Должность не соответствует ни одной из ролей. " +
            $"IsAdvisor={employee?.IsAdvisor ?? false}. " +
            $"Должности: {string.Join(", ", positions.Select(ep => $"{ep.Position?.Title} / {ep.OrgUnit?.Title}"))}");
    }

    public async Task<AuthResult<IReadOnlyList<StudentSsoDetailDto>>> GetAdvisorStudentsAsync(int advisorId, CancellationToken ct = default)
    {
        var employee = await _ssoDb.Edu_Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.ID == advisorId && e.IsAdvisor, ct);

        if (employee is null)
            return AuthResult<IReadOnlyList<StudentSsoDetailDto>>.NotFound("Эдвайзер не найден.");

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
            return AuthResult<IReadOnlyList<StudentSsoDetailDto>>.Success(Array.Empty<StudentSsoDetailDto>());

        var studentIins = advisorStudents
            .Where(s => s.User?.IIN != null)
            .Select(s => s.User!.IIN!)
            .ToList();

        var epvoStudents = await GetEpvoStudentsByIinsAsync(studentIins, ct);
        var epvoByIin = epvoStudents.ToDictionary(x => x.IinPlt ?? "", x => x);

        var result = advisorStudents.Select(s =>
        {
            var iin = s.User?.IIN ?? "";
            if (epvoByIin.TryGetValue(iin, out var epvo))
                return epvo;

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

        return AuthResult<IReadOnlyList<StudentSsoDetailDto>>.Success(result);
    }

    public async Task<AuthResult<IReadOnlyList<StudentSsoDetailDto>>> GetDirectorStudentsAsync(int userId, CancellationToken ct = default)
    {
        var directorPosition = await _ssoDb.Edu_EmployeePositions
            .AsNoTracking()
            .Include(ep => ep.Position)
            .Include(ep => ep.OrgUnit)
            .Where(ep => ep.EmployeeID == userId &&
                         ep.Position != null && !ep.Position.Deleted &&
                         ep.Position.Title == "Директор Института")
            .FirstOrDefaultAsync(ct);

        if (directorPosition is null)
            return AuthResult<IReadOnlyList<StudentSsoDetailDto>>.NotFound("Директор института не найден.");

        var instituteName = directorPosition.OrgUnit?.Title ?? "";

        var allStudents = await GetAllEpvoStudentsAsync(ct);

        var result = allStudents
            .Where(s => !string.IsNullOrEmpty(s.FacultyName) &&
                        s.FacultyName.Contains(instituteName, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return AuthResult<IReadOnlyList<StudentSsoDetailDto>>.Success(result);
    }

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
