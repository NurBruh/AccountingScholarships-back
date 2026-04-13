using AccountingScholarships.Application.Common;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AccountingScholarships.Infrastructure.Repositories;

/// <summary>
/// Вызов хранимых процедур через EpvoSsoDbContext (EPVO_test).
/// </summary>
public class StoredProcedureRepository : IStoredProcedureRepository
{
    private readonly EpvoSsoDbContext _context;
    private readonly string _dataSource;

    public StoredProcedureRepository(EpvoSsoDbContext context, Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        _context = context;
        _dataSource = configuration["SyncSettings:EpvoDataSource"] ?? "Dump";
    }

    public async Task<StoredProcedureResult> ExecuteReloadStudentAsync(CancellationToken ct = default)
    {
        // 1. Запоминаем ID-шки студентов ДО выполнения процедуры
        var existingIds = await _context.Student_Sso
            .AsNoTracking()
            .Select(s => s.StudentId)
            .ToListAsync(ct);

        var existingIdSet = new HashSet<int>(existingIds);

        // 2. Выполняем хранимую процедуру
        var returnValueParam = new SqlParameter
        {
            ParameterName = "@ReturnValue",
            SqlDbType = System.Data.SqlDbType.Int,
            Direction = System.Data.ParameterDirection.Output
        };

        var rowsAffected = await _context.Database
            .ExecuteSqlRawAsync(
                "EXEC @ReturnValue = [dbo].[Reload_STUDENT]",
                new[] { returnValueParam },
                ct);

        var returnValue = (int)returnValueParam.Value;

        // 3. Забираем НОВЫЕ записи (которых не было до выполнения)
        var insertedStudents = await _context.Student_Sso
            .AsNoTracking()
            .Where(s => !existingIdSet.Contains(s.StudentId))
            .ToListAsync(ct);

        return new StoredProcedureResult
        {
            ReturnValue = returnValue,
            RowsAffected = rowsAffected,
            ExecutedAt = DateTime.UtcNow,
            Message = returnValue == 0
                ? $"Успешно выполнено. Обработано записей: {rowsAffected}. Новых студентов: {insertedStudents.Count}."
                : $"Ошибка выполнения. Код возврата: {returnValue}.",
            InsertedStudents = insertedStudents
        };
    }

    public async Task<List<Dictionary<string, object?>>> ReadReloadStudentAsync(CancellationToken ct = default)
    {
        var results = new List<Dictionary<string, object?>>();

        var connection = _context.Database.GetDbConnection();
        await connection.OpenAsync(ct);

        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = "EXEC [dbo].[Reload_STUDENT]";
            command.CommandTimeout = 120;

            using var reader = await command.ExecuteReaderAsync(ct);

            while (await reader.ReadAsync(ct))
            {
                var row = new Dictionary<string, object?>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }
                results.Add(row);
            }
        }
        finally
        {
            await connection.CloseAsync();
        }

        return results;
    }

    public async Task<int> SaveReloadStudentToTempAsync(CancellationToken ct = default)
    {
        // 1. Читаем результат SP
        var rows = await ReadReloadStudentAsync(ct);

        // 2. Маппим в сущности Student_Temp
        var entities = rows.Select(MapRowToStudentTemp).ToList();

        // 3. Очищаем STUDENT_TEMP
        await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [dbo].[STUDENT_TEMP]", ct);

        // 4. Вставляем записи
        await _context.Student_Temp.AddRangeAsync(entities, ct);
        await _context.SaveChangesAsync(ct);

        return entities.Count;
    }

    public async Task<SendTempResult> SendTempToEpvoAsync(string triggeredBy, CancellationToken ct = default)
    {
        // 1. Читаем все записи из STUDENT_TEMP
        var tempStudents = await _context.Student_Temp
            .AsNoTracking()
            .ToListAsync(ct);

        if (tempStudents.Count == 0)
        {
            return new SendTempResult
            {
                Total = 0, Success = 0, Errors = 0,
                Message = "STUDENT_TEMP пуст — нечего синхронизировать."
            };
        }

        int success = 0;
        int errors = 0;
        var logs = new List<StudentSyncLog>();

        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        // 2. Загружаем существующие записи из STUDENT_DUMP в память и строим словарь
        //    Избегаем Contains(list) чтобы не генерировать OPENJSON, несовместимый
        //    со старым уровнем совместимости SQL Server.
        var tempIds = new HashSet<int>(tempStudents.Select(s => s.StudentId));
        var allDumps = await _context.Student_Dumps
            .AsNoTracking()
            .ToListAsync(ct);
        var existingDumps = allDumps
            .Where(d => tempIds.Contains(d.StudentId))
            .ToDictionary(d => d.StudentId);

        // 3. UPSERT: обновляем существующие, добавляем новые
        foreach (var temp in tempStudents)
        {
            var log = new StudentSyncLog
            {
                StudentId    = temp.StudentId,
                IinPlt       = temp.IinPlt,
                SentAt       = DateTime.UtcNow,
                EpvoEndpoint = "STUDENT_DUMP (local sync)",
                RequestBody  = JsonSerializer.Serialize(temp, jsonOptions),
                TriggeredBy  = triggeredBy
            };

            try
            {
                if (existingDumps.TryGetValue(temp.StudentId, out var existing))
                {
                    // UPDATE — копируем все поля из TEMP в DUMP
                    CopyTempToDump(temp, existing);
                }
                else
                {
                    // INSERT — создаём новую запись в STUDENT_DUMP
                    var newDump = new Student_Dump();
                    CopyTempToDump(temp, newDump);
                    _context.Student_Dumps.Add(newDump);
                }

                log.Status       = "Success";
                log.ResponseBody = "{\"result\":\"synced_to_dump\"}";
                success++;
            }
            catch (Exception ex)
            {
                log.Status       = "Error";
                log.ErrorMessage = ex.Message;
                errors++;
            }

            logs.Add(log);
        }

        // 4. Сохраняем изменения в STUDENT_DUMP и логи одним батчем
        await _context.StudentSyncLogs.AddRangeAsync(logs, ct);
        await _context.SaveChangesAsync(ct);

        return new SendTempResult
        {
            Total   = tempStudents.Count,
            Success = success,
            Errors  = errors,
            Message = $"STUDENT_DUMP обновлён. Обработано: {tempStudents.Count}. Успешно: {success}. Ошибок: {errors}."
        };
    }

    /// <summary>
    /// Копирует все поля из Student_Temp в Student_Dump.
    /// </summary>
    private static void CopyTempToDump(Student_Temp src, Student_Dump dst)
    {
        dst.UniversityId              = src.UniversityId;
        dst.StudentId                 = src.StudentId;
        dst.FirstName                 = src.FirstName;
        dst.LastName                  = src.LastName;
        dst.Patronymic                = src.Patronymic;
        dst.BirthDate                 = src.BirthDate;
        dst.StartDate                 = src.StartDate;
        dst.Address                   = src.Address;
        dst.NationId                  = src.NationId;
        dst.StudyFormId               = src.StudyFormId;
        dst.StudyCalendarId           = src.StudyCalendarId;
        dst.PaymentFormId             = src.PaymentFormId;
        dst.StudyLanguageId           = src.StudyLanguageId;
        dst.Photo                     = src.Photo;
        dst.ProfessionId              = src.ProfessionId;
        dst.CourseNumber              = src.CourseNumber;
        dst.TranscriptNumber          = src.TranscriptNumber;
        dst.TranscriptSeries          = src.TranscriptSeries;
        dst.IsMarried                 = src.IsMarried;
        dst.IcNumber                  = src.IcNumber;
        dst.IcDate                    = src.IcDate;
        dst.Education                 = src.Education;
        dst.HasExcellent              = src.HasExcellent;
        dst.StartOrder                = src.StartOrder;
        dst.IsStudent                 = src.IsStudent;
        dst.Certificate               = src.Certificate;
        dst.GrantNumber               = src.GrantNumber;
        dst.Gpa                       = src.Gpa;
        dst.CurrentCreditsSum         = src.CurrentCreditsSum;
        dst.Residence                 = src.Residence;
        dst.SitizenshipId             = src.SitizenshipId;
        dst.DormState                 = src.DormState;
        dst.IsInRetire                = src.IsInRetire;
        dst.FromId                    = src.FromId;
        dst.Local                     = src.Local;
        dst.City                      = src.City;
        dst.ContractId                = src.ContractId;
        dst.SpecializationId          = src.SpecializationId;
        dst.IinPlt                    = src.IinPlt;
        dst.AltynBelgi                = src.AltynBelgi;
        dst.DataVydachiAttestata      = src.DataVydachiAttestata;
        dst.DataVydachiDiploma        = src.DataVydachiDiploma;
        dst.DateDocEducation          = src.DateDocEducation;
        dst.EndCollege                = src.EndCollege;
        dst.EndHighSchool             = src.EndHighSchool;
        dst.EndSchool                 = src.EndSchool;
        dst.IcSeries                  = src.IcSeries;
        dst.IcType                    = src.IcType;
        dst.LivingAddress             = src.LivingAddress;
        dst.NomerAttestata            = src.NomerAttestata;
        dst.OtherBirthPlace           = src.OtherBirthPlace;
        dst.SeriesNumberDocEducation  = src.SeriesNumberDocEducation;
        dst.SeriyaAttestata           = src.SeriyaAttestata;
        dst.SeriyaDiploma             = src.SeriyaDiploma;
        dst.SchoolName                = src.SchoolName;
        dst.FacultyId                 = src.FacultyId;
        dst.SexId                     = src.SexId;
        dst.Mail                      = src.Mail;
        dst.Phone                     = src.Phone;
        dst.SumPoints                 = src.SumPoints;
        dst.SumPointsCreative         = src.SumPointsCreative;
        dst.EnrollOrderDate           = src.EnrollOrderDate;
        dst.MobilePhone               = src.MobilePhone;
        dst.GrantType                 = src.GrantType;
        dst.AcademicMobility          = src.AcademicMobility;
        dst.IncorrectIin              = src.IncorrectIin;
        dst.BirthPlaceCatoId          = src.BirthPlaceCatoId;
        dst.LivingPlaceCatoId         = src.LivingPlaceCatoId;
        dst.RegistrationPlaceCatoId   = src.RegistrationPlaceCatoId;
        dst.NaselennyiPunktAttestataCatoId = src.NaselennyiPunktAttestataCatoId;
        dst.EnterExamType             = src.EnterExamType;
        dst.FundingId                 = src.FundingId;
        dst.TypeCode                  = src.TypeCode;
    }

    private static Student_Temp MapRowToStudentTemp(Dictionary<string, object?> row)
    {
        return new Student_Temp
        {
            UniversityId          = GetInt(row, "universityId"),
            StudentId             = GetInt(row, "studentId") ?? 0,
            FirstName             = GetStr(row, "firstname"),
            LastName              = GetStr(row, "lastname"),
            Patronymic            = GetStr(row, "patronymic"),
            BirthDate             = GetDate(row, "birthDate"),
            StartDate             = GetDate(row, "startDate"),
            Address               = GetStr(row, "address"),
            NationId              = GetInt(row, "nationid"),
            StudyFormId           = GetInt(row, "studyformid"),
            PaymentFormId         = GetInt(row, "paymentformid"),
            StudyLanguageId       = GetInt(row, "studylanguageid"),
            Photo                 = null,
            ProfessionId          = GetInt(row, "professionid"),
            CourseNumber          = GetInt(row, "coursenumber"),
            TranscriptNumber      = GetStr(row, "transcriptNumber"),
            TranscriptSeries      = GetStr(row, "transcriptSeries"),
            IsMarried             = GetInt(row, "ismarried"),
            IcNumber              = GetStr(row, "icnumber"),
            IcDate                = GetDate(row, "icDate"),
            Education             = GetStr(row, "education"),
            HasExcellent          = GetBool(row, "hasexcellent"),
            StartOrder            = GetStr(row, "startorder"),
            IsStudent             = GetInt(row, "isstudent"),
            Certificate           = GetStr(row, "certificate"),
            GrantNumber           = GetStr(row, "grantnumber"),
            Gpa                   = GetDecimal(row, "gpa"),
            CurrentCreditsSum     = GetFloat(row, "currentCreditsSum"),
            Residence             = GetInt(row, "residence"),
            SitizenshipId         = GetInt(row, "sitizenshipid"),
            DormState             = GetInt(row, "dormState"),
            IsInRetire            = GetBool(row, "isinretire"),
            FromId                = GetInt(row, "fromid"),
            Local                 = GetBool(row, "local"),
            City                  = GetStr(row, "city"),
            ContractId            = GetInt(row, "contractid"),
            SpecializationId      = GetInt(row, "specializationid"),
            IinPlt                = GetStr(row, "iinplt"),
            AltynBelgi            = GetBool(row, "altynBelgi"),
            DataVydachiAttestata  = GetDate(row, "datavydachiattestata"),
            DataVydachiDiploma    = GetDate(row, "datavydachidiploma"),
            DateDocEducation      = GetDate(row, "dateDocEducation"),
            EndCollege            = GetBool(row, "endCollege"),
            EndHighSchool         = GetBool(row, "endHighSchool"),
            EndSchool             = GetBool(row, "endSchool"),
            IcSeries              = GetStr(row, "icseries"),
            IcType                = GetInt(row, "ictype"),
            LivingAddress         = GetStr(row, "livingAddress"),
            NomerAttestata        = GetStr(row, "nomerattestata"),
            OtherBirthPlace       = GetStr(row, "otherBirthPlace"),
            SeriesNumberDocEducation = GetStr(row, "seriesNumberDocEducation"),
            SeriyaAttestata       = GetStr(row, "seriyaattestata"),
            SeriyaDiploma         = GetStr(row, "seriyaDiploma"),
            SchoolName            = GetStr(row, "schoolName"),
            FacultyId             = GetInt(row, "facultyId"),
            SexId                 = GetInt(row, "sexid"),
            Mail                  = GetStr(row, "mail"),
            Phone                 = GetStr(row, "phone"),
            SumPoints             = GetInt(row, "sumPoints"),
            SumPointsCreative     = GetInt(row, "sumPointsCreative"),
            EnrollOrderDate       = GetDate(row, "enrollOrderDate"),
            MobilePhone           = GetStr(row, "mobilePhone"),
            GrantType             = GetInt(row, "grant_type"),
            AcademicMobility      = GetInt(row, "academicMobility"),
            IncorrectIin          = GetBool(row, "incorrectIin"),
            BirthPlaceCatoId      = GetInt(row, "birthPlaceCatoId"),
            LivingPlaceCatoId     = GetInt(row, "livingPlaceCatoId"),
            RegistrationPlaceCatoId = GetInt(row, "registrationPlaceCatoId"),
            NaselennyiPunktAttestataCatoId = GetInt(row, "naselennyiPunktAttestataCatoId"),
            FundingId             = GetInt(row, "fundingId"),
            TypeCode              = GetStr(row, "typeCode"),
        };
    }

    private static int? GetInt(Dictionary<string, object?> row, string key)
    {
        var val = GetRaw(row, key);
        if (val == null) return null;
        try { return Convert.ToInt32(val); } catch { return null; }
    }

    private static string? GetStr(Dictionary<string, object?> row, string key)
    {
        var val = GetRaw(row, key);
        return val?.ToString();
    }

    private static decimal? GetDecimal(Dictionary<string, object?> row, string key)
    {
        var val = GetRaw(row, key);
        if (val == null) return null;
        try { return Convert.ToDecimal(val); } catch { return null; }
    }

    private static float? GetFloat(Dictionary<string, object?> row, string key)
    {
        var val = GetRaw(row, key);
        if (val == null) return null;
        try { return Convert.ToSingle(val); } catch { return null; }
    }

    private static bool? GetBool(Dictionary<string, object?> row, string key)
    {
        var val = GetRaw(row, key);
        if (val == null) return null;
        if (val is bool b) return b;
        if (val is int i) return i != 0;
        if (val is byte by) return by != 0;
        try { return Convert.ToBoolean(val); } catch { return null; }
    }

    private static DateOnly? GetDate(Dictionary<string, object?> row, string key)
    {
        var val = GetRaw(row, key);
        if (val == null) return null;
        if (val is DateOnly d) return d;
        if (val is DateTime dt) return DateOnly.FromDateTime(dt);
        if (DateTime.TryParse(val.ToString(), out var parsed)) return DateOnly.FromDateTime(parsed);
        return null;
    }

    public async Task<SyncLogPagedResult> GetSyncLogsAsync(string? status, int page, int pageSize, CancellationToken ct = default)
    {
        var query = _context.StudentSyncLogs.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(x => x.Status == status);

        var total = await query.CountAsync(ct);

        var logs = await query
            .OrderByDescending(x => x.SentAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new SyncLogPagedResult
        {
            Total = total,
            Page = page,
            PageSize = pageSize,
            Logs = logs
        };
    }

    public async Task<IReadOnlyList<StudentSyncLog>> GetSyncLogsByStudentAsync(int studentId, CancellationToken ct = default)
    {
        return await _context.StudentSyncLogs
            .AsNoTracking()
            .Where(x => x.StudentId == studentId)
            .OrderByDescending(x => x.SentAt)
            .ToListAsync(ct);
    }

    private static object? GetRaw(Dictionary<string, object?> row, string key)
    {
        var entry = row.FirstOrDefault(kv =>
            string.Equals(kv.Key, key, StringComparison.OrdinalIgnoreCase));
        var val = entry.Value;
        if (val == null || val is DBNull) return null;
        return val;
    }

    // ─── Sync Preview ─────────────────────────────────────────────

    public async Task<SyncPreviewResult> GetSyncPreviewAsync(CancellationToken ct = default)
    {
        // 1. Читаем результат SP (read-only, ничего не пишем)
        var rows = await ReadReloadStudentAsync(ct);

        // 2. Загружаем справочники факультетов и профессий
        var facultyDict = await _context.Faculties
            .AsNoTracking()
            .Where(f => f.FacultyNameRu != null)
            .ToDictionaryAsync(f => f.FacultyId, f => f.FacultyNameRu!, ct);

        var professionDict = await _context.Professions
            .AsNoTracking()
            .Where(p => p.ProfessionNameRu != null)
            .ToDictionaryAsync(p => p.ProfessionId, p => p.ProfessionNameRu!, ct);

        // 3. Загружаем существующие IIN из STUDENT_DUMP/STUDENT_SSO и STUDENT в HashSet
        List<string> iinsInSso;
        if (_dataSource.Equals("Sso", StringComparison.OrdinalIgnoreCase))
        {
            iinsInSso = await _context.Student_Sso
                .AsNoTracking()
                .Where(s => s.IinPlt != null)
                .Select(s => s.IinPlt!)
                .ToListAsync(ct);
        }
        else
        {
            iinsInSso = await _context.Student_Dumps
                .AsNoTracking()
                .Where(s => s.IinPlt != null)
                .Select(s => s.IinPlt!)
                .ToListAsync(ct);
        }

        var iinsInStudent = await _context.Students
            .AsNoTracking()
            .Where(s => s.IinPlt != null)
            .Select(s => s.IinPlt!)
            .ToListAsync(ct);

        var ssoSet     = new HashSet<string>(iinsInSso,     StringComparer.OrdinalIgnoreCase);
        var studentSet = new HashSet<string>(iinsInStudent, StringComparer.OrdinalIgnoreCase);

        // 3. Формируем предпросмотр с флагом дубликата
        var items = rows.Select(row =>
        {
            var iin = GetStr(row, "iinplt");
            var professionId = GetInt(row, "professionid");

            string? duplicateSource = null;
            if (iin != null && ssoSet.Contains(iin))
                duplicateSource = _dataSource.Equals("Sso", StringComparison.OrdinalIgnoreCase) ? "STUDENT_SSO" : "STUDENT_DUMP";
            else if (iin != null && studentSet.Contains(iin))
                duplicateSource = "STUDENT";

            // Разбираем paymentFormId → текст
            var paymentFormId = GetInt(row, "paymentformid");
            var paymentType = paymentFormId == 2 ? "Стипендия"
                            : paymentFormId == 1 ? "Платник" : null;

            // Разбираем grantType → текст
            var grantTypeId = GetInt(row, "grant_type");
            var grantType = grantTypeId == -4 ? "Государственный грант"
                          : grantTypeId == -7 ? "Из собственных средств"
                          : grantTypeId == -6 ? "Трехсторонняя форма обучения" : null;

            var lastName  = GetStr(row, "lastname")  ?? "";
            var firstName = GetStr(row, "firstname") ?? "";
            var patronymic = GetStr(row, "patronymic") ?? "";
            var fullName  = $"{lastName} {firstName} {patronymic}".Trim();

            facultyDict.TryGetValue(GetInt(row, "facultyId") ?? 0, out var facultyName);
            professionDict.TryGetValue(GetInt(row, "professionid") ?? 0, out var professionName);

            return new SyncPreviewItem
            {
                StudentId       = GetInt(row, "studentid"),
                IinPlt          = iin,
                FullName        = fullName,
                CourseNumber    = GetInt(row, "coursenumber"),
                FacultyName     = facultyName,
                ProfessionName  = professionName,
                PaymentType     = paymentType,
                GrantType       = grantType,
                IsDuplicate     = duplicateSource != null,
                DuplicateSource = duplicateSource,
            };
        }).ToList();

        return new SyncPreviewResult
        {
            Total          = items.Count,
            NewCount       = items.Count(x => !x.IsDuplicate),
            DuplicateCount = items.Count(x => x.IsDuplicate),
            Items          = items,
        };
    }
}
