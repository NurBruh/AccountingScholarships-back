using AccountingScholarships.Application.DTO;
using AccountingScholarships.Application.DTO.EpvoSso.EpvoJoin;
using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AccountingScholarships.Infrastructure.Repositories;

public class ComparisonRepository : IComparisonRepository
{
    private readonly SsoDbContext _ssoContext;
    private readonly EpvoSsoDbContext _epvoContext;
    private readonly IChangeLogRepository _changeLogRepo;
    private readonly string _dataSource;

    public ComparisonRepository(
        SsoDbContext ssoContext,
        EpvoSsoDbContext epvoContext,
        IChangeLogRepository changeLogRepo,
        IConfiguration configuration)
    {
        _ssoContext = ssoContext;
        _epvoContext = epvoContext;
        _changeLogRepo = changeLogRepo;
        _dataSource = configuration["SyncSettings:EpvoDataSource"] ?? "Dump";
    }

    public async Task<IReadOnlyList<StudentComparisonDto>> GetComparisonAsync(CancellationToken ct = default)
    {
        var ssoTask = LoadSsoDataAsync(ct);
        var epvoStudents = await LoadEpvoDataAsync(ct);
        var studyForms = await LoadStudyFormsAsync(ct);

        var ssoStudents = await ssoTask;

        var ssoStudentIds = ssoStudents.Select(s => s.StudentID).ToHashSet();

        // Загружаем все записи без Contains (избегаем OPENJSON для совместимости с SQL Server < 2016)
        var ssiList = await _ssoContext.Scollarship_Students_Infos
            .AsNoTracking()
            .Where(x => x.studentID != null)
            .ToListAsync(ct);

        var ssiDict = ssiList
            .Where(x => ssoStudentIds.Contains(x.studentID!.Value))
            .GroupBy(x => x.studentID!.Value)
            .ToDictionary(g => g.Key, g => g.First());

        // Мёрджим iic/updated_date в SSO студентов
        foreach (var s in ssoStudents)
        {
            if (ssiDict.TryGetValue(s.StudentID, out var ssi))
            {
                s.Iic = ssi.Iic;
                s.UpdatedDate = ssi.Updated_Date;
            }
        }

        // Индексируем ЕПВО данные по IIN
        var epvoByIin = epvoStudents
            .Where(e => !string.IsNullOrEmpty(e.IinPlt))
            .GroupBy(e => e.IinPlt!)
            .ToDictionary(g => g.Key, g => g.First());

        // Собираем все IIN из обоих источников
        var allIins = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var s in ssoStudents.Where(s => !string.IsNullOrEmpty(s.IIN)))
            allIins.Add(s.IIN!);
        foreach (var e in epvoStudents.Where(e => !string.IsNullOrEmpty(e.IinPlt)))
            allIins.Add(e.IinPlt!);

        // Индексируем ССО данные по IIN
        var ssoByIin = ssoStudents
            .Where(s => !string.IsNullOrEmpty(s.IIN))
            .GroupBy(s => s.IIN!, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);

        var result = new List<StudentComparisonDto>();
        var changeLogs = new List<StudentChangeLog>();
        var sessionId = Guid.NewGuid().ToString("N")[..16];

        foreach (var iin in allIins)
        {
            ssoByIin.TryGetValue(iin, out var sso);
            epvoByIin.TryGetValue(iin, out var epvo);

            var dto = new StudentComparisonDto
            {
                StudentId = sso?.StudentID ?? epvo?.StudentId ?? 0,
                IIN = iin
            };

            // ССО данные (трансформированные)
            if (sso != null)
            {
                dto.Sso_FullName = sso.FullName;
                dto.Sso_CourseNumber = sso.Year;
                dto.Sso_StudyForm = ResolveStudyFormName(
                    ComputeStudyFormId(sso.LevelID, sso.SemesterCount, sso.EducationTypeID),
                    studyForms);
                dto.Sso_Institute = sso.InstituteName;
                dto.Sso_Cafedra = sso.CafedraName;
                dto.Sso_Speciality = sso.SpecialityName;
                dto.Sso_PaymentType = sso.EducationPaymentTypeID == 1 ? "Стипендия" : "Платник";
                dto.Sso_GrantType = ResolveGrantTypeLabel(
                    ComputeGrantType(sso.EptESUVOGrantTypeId, sso.GtypeESUVOGrantTypeId));
                dto.Sso_Iic = sso.Iic;
                dto.Sso_UpdatedDate = sso.UpdatedDate;
            }

            // ЕПВО данные
            if (epvo != null)
            {
                dto.Epvo_FullName = epvo.FullName;
                dto.Epvo_CourseNumber = epvo.CourseNumber;
                dto.Epvo_StudyForm = epvo.StudyForm;
                dto.Epvo_FacultyName = epvo.FacultyName;
                dto.Epvo_Specialization = epvo.Specialization;
                dto.Epvo_PaymentType = epvo.PaymentType;
                dto.Epvo_GrantType = epvo.GrantType;
                dto.Epvo_Iic = epvo.Iic;
                dto.Epvo_UpdateDate = epvo.UpdateDate;
            }

            // Логика приоритета по ментору:
            // Если IIC-и разные И дата в Scollarship_Students_Info свежее → SSO приоритет
            if (sso != null && epvo != null)
            {
                var iicsDifferent = !string.Equals(
                    (sso.Iic ?? "").Trim(),
                    (epvo.Iic ?? "").Trim(),
                    StringComparison.OrdinalIgnoreCase);

                var ssoDateFresher = sso.UpdatedDate.HasValue && epvo.UpdateDate.HasValue
                    ? sso.UpdatedDate.Value > epvo.UpdateDate.Value.ToDateTime(TimeOnly.MinValue)
                    : sso.UpdatedDate.HasValue;

                if (iicsDifferent && ssoDateFresher)
                    dto.Priority = "SSO";
                else if (iicsDifferent && !ssoDateFresher)
                    dto.Priority = "EPVO";
                else
                    dto.Priority = "OK";
            }

            // Сравнение полей с записью в лог изменений
            if (sso == null || epvo == null)
            {
                dto.HasDifference = true;
                dto.DifferentFields.Add(sso == null ? "Нет в ССО" : "Нет в ЕПВО");
            }
            else
            {
                CompareAndLog(dto, changeLogs, iin, sessionId, "ФИО", dto.Sso_FullName, dto.Epvo_FullName);
                CompareAndLog(dto, changeLogs, iin, sessionId, "Курс", dto.Sso_CourseNumber?.ToString(), dto.Epvo_CourseNumber?.ToString());
                CompareAndLog(dto, changeLogs, iin, sessionId, "Форма обучения", dto.Sso_StudyForm, dto.Epvo_StudyForm);
                CompareAndLog(dto, changeLogs, iin, sessionId, "Институт", dto.Sso_Institute, dto.Epvo_FacultyName);
                CompareAndLog(dto, changeLogs, iin, sessionId, "Специализация", dto.Sso_Speciality, dto.Epvo_Specialization);
                CompareAndLog(dto, changeLogs, iin, sessionId, "Тип оплаты", dto.Sso_PaymentType, dto.Epvo_PaymentType);
                CompareAndLog(dto, changeLogs, iin, sessionId, "Тип гранта", dto.Sso_GrantType, dto.Epvo_GrantType);
                CompareAndLog(dto, changeLogs, iin, sessionId, "ИИК (Р/С)", dto.Sso_Iic, dto.Epvo_Iic);
            }

            result.Add(dto);
        }

        return result.AsReadOnly();
    }

    #region SSO Data Loading

    private async Task<List<SsoStudentFlat>> LoadSsoDataAsync(CancellationToken ct)
    {
        return await _ssoContext.Edu_Students
            .AsNoTracking()
            .Where(s => s.StatusID != 2 && s.StatusID != null && s.CategoryID == 1 && s.User.DOB != null && s.EducationPaymentTypeID == 1)
            .Select(s => new SsoStudentFlat
            {
                StudentID = s.StudentID,
                IIN = s.User.IIN,
                FullName = s.User != null
                    ? (s.User.LastName + " " + (s.User.FirstName ?? "") + " " + (s.User.MiddleName ?? "")).Trim()
                    : null,
                Year = s.Year,
                EducationTypeID = s.EducationTypeID,
                EducationPaymentTypeID = s.EducationPaymentTypeID,
                LevelID = s.Speciality != null ? s.Speciality.LevelID : 0,
                SemesterCount = s.Rup != null ? s.Rup.SemesterCount : 0,
                CafedraName = s.Speciality != null && s.Speciality.RupEditorOrgUnit != null
                    ? s.Speciality.RupEditorOrgUnit.Title
                    : null,
                InstituteName = s.Speciality != null
                    && s.Speciality.RupEditorOrgUnit != null
                    && s.Speciality.RupEditorOrgUnit.Parent != null
                    && s.Speciality.RupEditorOrgUnit.Parent.TypeID == 2
                        ? s.Speciality.RupEditorOrgUnit.Parent.Title
                        : null,
                SpecialityName = s.Speciality != null ? s.Speciality.Title : null,
                EptESUVOGrantTypeId = s.EducationPaymentType != null
                    ? s.EducationPaymentType.ESUVOGrantTypeId
                    : null,
                GtypeESUVOGrantTypeId = s.GrantType != null
                    ? s.GrantType.ESUVOGrantTypeId
                    : null
            })
            .AsSplitQuery()
            .ToListAsync(ct);
    }

    #endregion

    #region EPVO Data Loading

    private async Task<List<StudentSsoDetailDto>> LoadEpvoDataAsync(CancellationToken ct)
    {
        // Переключение между STUDENT_DUMP (полигон) и STUDENT_SSO (продакшн)
        var tableName = _dataSource.Equals("Sso", StringComparison.OrdinalIgnoreCase)
            ? "STUDENT_SSO"
            : "STUDENT_DUMP";

        var sql = $"""
            SELECT 
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
            FROM {tableName} ss
            LEFT JOIN STUDY_FORMS           sf  ON sf.id          = ss.studyFormId
            LEFT JOIN STUDYLANGUAGES        sl  ON sl.id          = ss.studyLanguageId
            LEFT JOIN PROFESSION            pe  ON pe.professionId = ss.professionid
            LEFT JOIN FACULTIES             fac ON fac.facultyId   = ss.facultyId
            LEFT JOIN SPECIALITIES_EPVO     se  ON se.id           = ss.specializationId
            LEFT JOIN STUDENT_INFO          si  ON si.studentId    = ss.studentId
            where ss.grantType = -4
            """;

        return await _epvoContext.Database
            .SqlQueryRaw<StudentSsoDetailDto>(sql)
            .ToListAsync(ct);
    }

    private async Task<Dictionary<int, string>> LoadStudyFormsAsync(CancellationToken ct)
    {
        return await _epvoContext.Study_forms
            .AsNoTracking()
            .Where(sf => sf.Id != null && sf.NameRu != null)
            .ToDictionaryAsync(sf => sf.Id!.Value, sf => sf.NameRu!, ct);
    }

    #endregion

    #region Transformation Logic (based on Reload_STUDENT stored procedure)

    /// <summary>
    /// Логика вычисления studyFormId из хранимой процедуры Reload_STUDENT
    /// </summary>
    private static int ComputeStudyFormId(int levelId, int semesterCount, int? educationTypeId)
    {
        if (levelId == 1 && educationTypeId == 1 && (semesterCount == 8 || semesterCount == 7))
            return 1; // дневное 4г
        if (levelId == 3)
            return 2; // дневное (PhD)
        if (levelId == 2 && semesterCount >= 3)
            return 3; // дневное (master) 2г
        if (levelId == 1 && educationTypeId == 2 && (semesterCount == 5 || semesterCount == 6))
            return 4; // заочное (ИДО_3г)
        if (levelId == 2 && semesterCount < 3)
            return 5;
        if (levelId == 1 && educationTypeId == 1 && (semesterCount == 5 || semesterCount == 6))
            return 6; // дневное 3г
        if (levelId == 1 && educationTypeId == 1 && (semesterCount == 10 || semesterCount == 9))
            return 7; // Дневное 5г
        if (levelId == 1 && educationTypeId == 2 && (semesterCount == 4 || semesterCount == 3))
            return 8; // заочное (ИДО_2г)
        if (levelId == 1 && educationTypeId == 2 && semesterCount == 7)
            return 10; // заочное (ИДО_3.5г)

        return 1; // default
    }

    private static string? ResolveStudyFormName(int studyFormId, Dictionary<int, string> studyForms)
    {
        return studyForms.TryGetValue(studyFormId, out var name) ? name : null;
    }

    /// <summary>
    /// Логика вычисления grantType из хранимой процедуры:
    /// ISNULL(IIF(ept.ESUVOGrantTypeId = 1, gtype.ESUVOGrantTypeId, ept.ESUVOGrantTypeId), 0)
    /// </summary>
    private static int ComputeGrantType(int? eptESUVOGrantTypeId, int? gtypeESUVOGrantTypeId)
    {
        if (eptESUVOGrantTypeId == 1)
            return gtypeESUVOGrantTypeId ?? 0;
        return eptESUVOGrantTypeId ?? 0;
    }

    private static string? ResolveGrantTypeLabel(int grantType)
    {
        return grantType switch
        {
            -4 => "Государственный грант",
            -7 => "Из собственных средств",
            -6 => "Трехсторонняя форма обучения",
            0 => null,
            _ => $"Грант ({grantType})"
        };
    }

    #endregion

    #region Comparison Helper

    private static void CompareAndLog(
        StudentComparisonDto dto,
        List<StudentChangeLog> logs,
        string iin,
        string sessionId,
        string fieldName,
        string? ssoValue,
        string? epvoValue)
    {
        var ssoNorm = (ssoValue ?? "").Trim();
        var epvoNorm = (epvoValue ?? "").Trim();

        if (!string.Equals(ssoNorm, epvoNorm, StringComparison.OrdinalIgnoreCase))
        {
            dto.HasDifference = true;
            dto.DifferentFields.Add(fieldName);

            logs.Add(new StudentChangeLog
            {
                IinPlt = iin,
                FieldName = fieldName,
                OldValue = epvoNorm,
                NewValue = ssoNorm,
                DataSource = dto.Priority ?? "N/A",
                ChangedAt = DateTime.Now,
                ChangedBy = "System",
                SyncSessionId = sessionId
            });
        }
    }

    #endregion

    #region Internal DTO

    private class SsoStudentFlat
    {
        public int StudentID { get; set; }
        public string? IIN { get; set; }
        public string? FullName { get; set; }
        public int Year { get; set; }
        public int? EducationTypeID { get; set; }
        public int? EducationPaymentTypeID { get; set; }
        public int LevelID { get; set; }
        public int SemesterCount { get; set; }
        public string? CafedraName { get; set; }
        public string? InstituteName { get; set; }
        public string? SpecialityName { get; set; }
        public int? EptESUVOGrantTypeId { get; set; }
        public int? GtypeESUVOGrantTypeId { get; set; }
        public string? Iic { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    #endregion

    public async Task<object> SyncBatchAsync(List<string> iins, string triggeredBy, CancellationToken ct = default)
    {
        // 1. Грузим данные из базы
        var ssoStudents = await LoadSsoDataAsync(ct);
        var epvoStudents = await LoadEpvoDataAsync(ct);

        var ssoStudentIds = ssoStudents.Where(s => s.IIN != null && iins.Contains(s.IIN)).Select(s => s.StudentID).ToHashSet();

        // Загружаем без Contains в SQL для совместимости с SQL Server < 2016
        var ssiDict = await _ssoContext.Scollarship_Students_Infos
            .AsNoTracking()
            .Where(x => x.studentID != null)
            .ToListAsync(ct)
            .ContinueWith(t => t.Result
                .Where(x => ssoStudentIds.Contains(x.studentID!.Value))
                .ToDictionary(x => x.studentID!.Value), ct);

        int updated = 0;
        var changeLogs = new List<StudentChangeLog>();
        var syncLogs = new List<StudentSyncLog>();
        var sessionId = Guid.NewGuid().ToString("N")[..16];

        foreach (var iin in iins)
        {
            var sso = ssoStudents.FirstOrDefault(s => s.IIN == iin);
            var epvo = epvoStudents.FirstOrDefault(e => e.IinPlt == iin);

            if (sso == null || epvo == null)
            {
                syncLogs.Add(new StudentSyncLog
                {
                    StudentId = epvo?.StudentId ?? sso?.StudentID ?? 0,
                    IinPlt = iin,
                    SentAt = DateTime.UtcNow,
                    Status = "Error",
                    ErrorMessage = "Студент не найден в одной из баз",
                    TriggeredBy = triggeredBy
                });
                continue;
            }

            if (ssiDict.TryGetValue(sso.StudentID, out var ssi))
            {
                sso.Iic = ssi.Iic;
                sso.UpdatedDate = ssi.Updated_Date;
            }

            var oldIic = epvo.Iic;
            var newIic = sso.Iic;

            // Если счет изменился
            if (newIic != null && newIic != oldIic)
            {
                try
                {
                    var sqlUpdate = @"
                        IF EXISTS (SELECT 1 FROM [STUDENT_INFO] WHERE studentId = @StudentId)
                            UPDATE [STUDENT_INFO] SET iic = @Iic, updateDate = @UpdateDate WHERE studentId = @StudentId
                        ELSE
                            INSERT INTO [STUDENT_INFO] (studentId, iic, updateDate) VALUES (@StudentId, @Iic, @UpdateDate)";

                    await _epvoContext.Database.ExecuteSqlRawAsync(sqlUpdate,
                        new Microsoft.Data.SqlClient.SqlParameter("@Iic", newIic),
                        new Microsoft.Data.SqlClient.SqlParameter("@UpdateDate", DateTime.Now),
                        new Microsoft.Data.SqlClient.SqlParameter("@StudentId", epvo.StudentId));

                    updated++;

                    // Собираем лог изменений
                    changeLogs.Add(new StudentChangeLog
                    {
                        IinPlt = iin,
                        FieldName = "ИИК (Р/С)",
                        OldValue = oldIic,
                        NewValue = newIic,
                        ChangedAt = DateTime.Now,
                        ChangedBy = triggeredBy,
                        DataSource = "SSO",
                        SyncSessionId = sessionId
                    });

                    // Собираем лог синхронизации
                    syncLogs.Add(new StudentSyncLog
                    {
                        StudentId = epvo.StudentId,
                        IinPlt = iin,
                        SentAt = DateTime.UtcNow,
                        Status = "Success",
                        RequestBody = $"{{ \"iic_updated\": \"{newIic}\" }}",
                        TriggeredBy = triggeredBy
                    });
                }
                catch (Exception ex)
                {
                    syncLogs.Add(new StudentSyncLog
                    {
                        StudentId = epvo.StudentId,
                        IinPlt = iin,
                        SentAt = DateTime.UtcNow,
                        Status = "Error",
                        ErrorMessage = ex.Message,
                        TriggeredBy = triggeredBy
                    });
                }
            }
        }

        // Сохраняем все логи
        if (changeLogs.Any())
            await _changeLogRepo.SaveChangesAsync(changeLogs, ct);

        if (syncLogs.Any())
        {
            await _epvoContext.StudentSyncLogs.AddRangeAsync(syncLogs, ct);
            await _epvoContext.SaveChangesAsync(ct);
        }

        return new { success = true, updated, errors = syncLogs.Count(s => s.Status == "Error"), logs = syncLogs };
    }
}
