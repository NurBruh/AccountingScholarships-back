using AccountingScholarships.Application.Common;
using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Infrastructure.Data;
using AccountingScholarships.Infrastructure.Services.StudentSync;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

/// <summary>
/// Репозиторий предпросмотра синхронизации.
/// Преобразует данные SSO → ЕПВО через C# (ISsoToEpvoMapperService),
/// затем сравнивает с текущими данными в STUDENT_DUMP (или STUDENT_SSO).
/// </summary>
public class PreviewRepository : IPreviewRepository
{
    private readonly EpvoSsoDbContext _context;
    private readonly ISsoToEpvoMapperService _mapper;
    private readonly string _dataSource;

    public PreviewRepository(
        EpvoSsoDbContext context,
        ISsoToEpvoMapperService mapper,
        Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _dataSource = configuration["SyncSettings:EpvoDataSource"] ?? "Dump";
    }

    public async Task<SyncPreviewResult> GetSyncPreviewAsync(CancellationToken ct = default)
    {
        // 1. Загружаем справочники
        var studyForms = await _context.Study_forms
            .AsNoTracking()
            .Where(x => x.NameRu != null && x.Id != null)
            .ToDictionaryAsync(x => x.Id!.Value, x => x.NameRu!, ct);

        var faculties = await _context.Faculties
            .AsNoTracking()
            .Where(x => x.FacultyNameRu != null)
            .ToDictionaryAsync(x => x.FacultyId, x => x.FacultyNameRu!, ct);

        var specialities = await _context.SpecialitiesEpvoNew
            .AsNoTracking()
            .Where(x => x.NameRu != null && x.Id != null)
            .ToDictionaryAsync(
                x => x.Id!, 
                x => $"{x.SpecializationCode ?? ""} {x.NameRu}".Trim(), 
                ct);

        var professions = await _context.Professions
            .AsNoTracking()
            .Where(p => p.ProfessionNameRu != null)
            .ToDictionaryAsync(p => p.ProfessionId, p => p.ProfessionNameRu!, ct);

        // 2. Преобразуем ВСЕХ активных студентов SSO → ЕПВО формат (C#, без SP)
        var ssoStudents = await _mapper.MapAllAsync(ct);

        // 3. Загружаем текущие данные из DUMP/Sso (+ STUDENT_INFO)
        var dumpRows = await LoadDumpOrSsoDataAsync(ct);
        var dumpById = dumpRows.ToDictionary(x => x.StudentId);

        // 4. Сравниваем и формируем результат
        var items = new List<SyncPreviewItem>();

        foreach (var sso in ssoStudents)
        {
            if (string.IsNullOrEmpty(sso.IinPlt)) continue;

            dumpById.TryGetValue(sso.StudentId, out var dump);
            var isNew = dump == null;

            var diffs = new List<FieldDiff>();

            if (!isNew)
            {
                diffs.AddRange(CompareKeyFields(sso, dump!, studyForms, faculties, specialities));
                if (diffs.Count == 0) continue; // совпадает — пропускаем
            }

            // DTO для сохранения в TEMP
            var dto = MapToDto(sso);

            // Имена для отображения
            var fullName = $"{sso.LastName} {sso.FirstName} {sso.Patronymic}".Trim();
            faculties.TryGetValue(sso.FacultyId ?? 0, out var facultyName);
            professions.TryGetValue(sso.ProfessionId ?? 0, out var professionName);

            var paymentType = sso.PaymentFormId == 2 ? "Стипендия"
                            : sso.PaymentFormId == 1 ? "Платник" : null;

            var grantType = ResolveGrantTypeLabel(sso.GrantType);

            items.Add(new SyncPreviewItem
            {
                StudentId = sso.StudentId,
                IinPlt = sso.IinPlt,
                FullName = fullName,
                CourseNumber = sso.CourseNumber,
                FacultyName = facultyName,
                ProfessionName = professionName,
                PaymentType = paymentType,
                GrantType = grantType,
                IsNew = isNew,
                DifferentFields = diffs.Select(d => d.FieldName).ToList(),
                FieldDiffs = diffs,
                Data = dto
            });
        }

        return new SyncPreviewResult
        {
            Total = items.Count,
            DiffCount = items.Count(x => !x.IsNew),
            NewCount = items.Count(x => x.IsNew),
            Items = items,
        };
    }

    #region EPVO Data Loading

    private async Task<List<DumpRow>> LoadDumpOrSsoDataAsync(CancellationToken ct)
    {
        var useSso = _dataSource.Equals("Sso", StringComparison.OrdinalIgnoreCase);
        var tableName = useSso ? "STUDENT_SSO" : "STUDENT_DUMP";

        var sql = $"""
            SELECT 
                d.studentId,
                d.lastName,
                d.firstName,
                d.patronymic,
                d.courseNumber,
                d.studyFormId,
                d.facultyId,
                d.specializationId,
                d.paymentFormId,
                d.grantType,
                i.iic,
                i.bic,
                i.updateDate
            FROM {tableName} d
            LEFT JOIN STUDENT_INFO i ON i.studentId = d.studentId
            """;

        return await _context.Database
            .SqlQueryRaw<DumpRow>(sql)
            .ToListAsync(ct);
    }

    private sealed class DumpRow
    {
        public int StudentId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Patronymic { get; set; }
        public int? CourseNumber { get; set; }
        public int? StudyFormId { get; set; }
        public int? FacultyId { get; set; }
        public int? SpecializationId { get; set; }
        public int? PaymentFormId { get; set; }
        public int? GrantType { get; set; }
        public string? Iic { get; set; }
        public string? Bic { get; set; }
        public DateOnly? UpdateDate { get; set; }
    }

    #endregion

    #region Comparison Logic

    private static List<FieldDiff> CompareKeyFields(
        Student_Temp sso,
        DumpRow dump,
        Dictionary<int, string> studyForms,
        Dictionary<int, string> faculties,
        Dictionary<string, string> specialities)
    {
        var diffs = new List<FieldDiff>();

        // ФИО
        var ssoFio = $"{sso.LastName} {sso.FirstName} {sso.Patronymic}".Trim();
        var epvoFio = $"{dump.LastName} {dump.FirstName} {dump.Patronymic}".Trim();
        if (!EqualNormalized(ssoFio, epvoFio))
            diffs.Add(new FieldDiff { FieldName = "ФИО", SsoValue = ssoFio, EpvoValue = epvoFio });

        // Курс
        if (!EqualNormalized(sso.CourseNumber?.ToString(), dump.CourseNumber?.ToString()))
            diffs.Add(new FieldDiff { FieldName = "Курс", SsoValue = sso.CourseNumber?.ToString(), EpvoValue = dump.CourseNumber?.ToString() });

        // Форма обучения
        var ssoStudyForm = sso.StudyFormId.HasValue && studyForms.TryGetValue(sso.StudyFormId.Value, out var sf) ? sf : null;
        var epvoStudyForm = dump.StudyFormId.HasValue && studyForms.TryGetValue(dump.StudyFormId.Value, out var ef) ? ef : null;
        if (!EqualNormalized(ssoStudyForm, epvoStudyForm))
            diffs.Add(new FieldDiff { FieldName = "Форма обучения", SsoValue = ssoStudyForm, EpvoValue = epvoStudyForm });

        // Институт
        var ssoFaculty = sso.FacultyId.HasValue && faculties.TryGetValue(sso.FacultyId.Value, out var fac) ? fac : null;
        var epvoFaculty = dump.FacultyId.HasValue && faculties.TryGetValue(dump.FacultyId.Value, out var efac) ? efac : null;
        if (!EqualNormalized(ssoFaculty, epvoFaculty))
            diffs.Add(new FieldDiff { FieldName = "Институт", SsoValue = ssoFaculty, EpvoValue = epvoFaculty });

        // Специализация
        var ssoSpec = sso.SpecializationId.HasValue && specialities.TryGetValue(sso.SpecializationId.Value.ToString(), out var sp) ? sp : null;
        var epvoSpec = dump.SpecializationId.HasValue && specialities.TryGetValue(dump.SpecializationId.Value.ToString(), out var esp) ? esp : null;
        if (!EqualNormalized(ssoSpec, epvoSpec))
            diffs.Add(new FieldDiff { FieldName = "Специализация", SsoValue = ssoSpec, EpvoValue = epvoSpec });

        // Тип оплаты
        var ssoPayment = sso.PaymentFormId == 2 ? "Стипендия" : sso.PaymentFormId == 1 ? "Платник" : null;
        var epvoPayment = dump.PaymentFormId == 2 ? "Стипендия" : dump.PaymentFormId == 1 ? "Платник" : null;
        if (!EqualNormalized(ssoPayment, epvoPayment))
            diffs.Add(new FieldDiff { FieldName = "Тип оплаты", SsoValue = ssoPayment, EpvoValue = epvoPayment });

        // Тип гранта
        var ssoGrant = ResolveGrantTypeLabel(sso.GrantType);
        var epvoGrant = ResolveGrantTypeLabel(dump.GrantType);
        if (!EqualNormalized(ssoGrant, epvoGrant))
            diffs.Add(new FieldDiff { FieldName = "Тип гранта", SsoValue = ssoGrant, EpvoValue = epvoGrant });

        return diffs;
    }

    private static string? ResolveGrantTypeLabel(int? grantType)
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

    private static bool EqualNormalized(string? a, string? b)
    {
        var na = Normalize(a);
        var nb = Normalize(b);
        return string.Equals(na, nb, StringComparison.OrdinalIgnoreCase);
    }

    private static string? Normalize(string? val)
    {
        if (string.IsNullOrWhiteSpace(val)) return null;
        var trimmed = val.Trim();
        if (trimmed.Equals("NA", StringComparison.OrdinalIgnoreCase)) return null;
        if (trimmed.Equals("N/A", StringComparison.OrdinalIgnoreCase)) return null;
        return trimmed;
    }

    #endregion

    #region DTO Mapping

    private static EpvoStudentTempDto MapToDto(Student_Temp src)
    {
        return new EpvoStudentTempDto
        {
            UniversityId = src.UniversityId,
            StudentId = src.StudentId,
            FirstName = src.FirstName,
            LastName = src.LastName,
            Patronymic = src.Patronymic,
            BirthDate = src.BirthDate,
            StartDate = src.StartDate,
            Address = src.Address,
            NationId = src.NationId,
            StudyFormId = src.StudyFormId,
            PaymentFormId = src.PaymentFormId,
            StudyLanguageId = src.StudyLanguageId,
            Photo = src.Photo,
            ProfessionId = src.ProfessionId,
            CourseNumber = src.CourseNumber,
            TranscriptNumber = src.TranscriptNumber,
            TranscriptSeries = src.TranscriptSeries,
            IsMarried = src.IsMarried,
            IcNumber = src.IcNumber,
            IcDate = src.IcDate,
            Education = src.Education,
            HasExcellent = src.HasExcellent,
            StartOrder = src.StartOrder,
            IsStudent = src.IsStudent,
            Certificate = src.Certificate,
            GrantNumber = src.GrantNumber,
            Gpa = src.Gpa,
            CurrentCreditsSum = src.CurrentCreditsSum,
            Residence = src.Residence,
            SitizenshipId = src.SitizenshipId,
            DormState = src.DormState,
            IsInRetire = src.IsInRetire,
            FromId = src.FromId,
            Local = src.Local,
            City = src.City,
            ContractId = src.ContractId,
            SpecializationId = src.SpecializationId,
            IinPlt = src.IinPlt,
            AltynBelgi = src.AltynBelgi,
            DataVydachiAttestata = src.DataVydachiAttestata,
            DataVydachiDiploma = src.DataVydachiDiploma,
            DateDocEducation = src.DateDocEducation,
            EndCollege = src.EndCollege,
            EndHighSchool = src.EndHighSchool,
            EndSchool = src.EndSchool,
            IcSeries = src.IcSeries,
            IcType = src.IcType,
            LivingAddress = src.LivingAddress,
            NomerAttestata = src.NomerAttestata,
            OtherBirthPlace = src.OtherBirthPlace,
            SeriesNumberDocEducation = src.SeriesNumberDocEducation,
            SeriyaAttestata = src.SeriyaAttestata,
            SeriyaDiploma = src.SeriyaDiploma,
            SchoolName = src.SchoolName,
            FacultyId = src.FacultyId,
            SexId = src.SexId,
            Mail = src.Mail,
            Phone = src.Phone,
            SumPoints = src.SumPoints,
            SumPointsCreative = src.SumPointsCreative,
            EnrollOrderDate = src.EnrollOrderDate,
            MobilePhone = src.MobilePhone,
            GrantType = src.GrantType,
            AcademicMobility = src.AcademicMobility,
            IncorrectIin = src.IncorrectIin,
            BirthPlaceCatoId = src.BirthPlaceCatoId,
            LivingPlaceCatoId = src.LivingPlaceCatoId,
            RegistrationPlaceCatoId = src.RegistrationPlaceCatoId,
            NaselennyiPunktAttestataCatoId = src.NaselennyiPunktAttestataCatoId,
            FundingId = src.FundingId,
            TypeCode = src.TypeCode
        };
    }

    #endregion
}
