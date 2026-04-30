using AccountingScholarships.Application.DTO;
using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Infrastructure.Data;
using AccountingScholarships.Infrastructure.Services.StudentSync;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class SyncPreviewRepository : ISyncPreviewRepository
{
    private readonly SsoDbContext _ssoContext;
    private readonly EpvoSsoDbContext _epvoContext;
    private readonly ISsoToEpvoMapperService _mapper;
    private readonly string _dataSource;

    public SyncPreviewRepository(
        SsoDbContext ssoContext,
        EpvoSsoDbContext epvoContext,
        ISsoToEpvoMapperService mapper,
        Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        _ssoContext = ssoContext;
        _epvoContext = epvoContext;
        _mapper = mapper;
        _dataSource = configuration["SyncSettings:EpvoDataSource"] ?? "Dump";
    }

    public async Task<SyncPreviewComparisonPagedDto> GetPreviewAsync(
        int page,
        int pageSize,
        string filter,
        string? search,
        CancellationToken ct = default)
    {
        // ── 1. Трансформируем ССО → ЕПВО (как Reload_STUDENT, но на C#) ──
        var ssoTemps = await _mapper.MapAllAsync(ct);

        // Оставляем только грантников (PaymentFormId == 2 ↔ EducationPaymentTypeID == 1 в ССО)
        ssoTemps = ssoTemps.Where(s => s.PaymentFormId == 2).ToList();

        var studentIds = ssoTemps.Select(s => s.StudentId).ToHashSet();
        var ssoStudentIdToInternal = ssoTemps
            .Where(s => s.StudentId >= 60000000)
            .ToDictionary(s => s.StudentId - 60000000, s => s.StudentId);

        // ── 2. Догружаем недостающие поля из ССО ──
        // 2a. Scollarship_Students_Info (ИИК, БИК, дата обновления)
        var ssiList = await _ssoContext.Scollarship_Students_Infos
            .AsNoTracking()
            .Where(x => x.studentID != null)
            .ToListAsync(ct);

        var ssiDict = ssiList
            .Where(x => x.studentID.HasValue)
            .GroupBy(x => x.studentID!.Value)
            .ToDictionary(g => g.Key, g => g.First());

        // 2b. FacultyId / InstituteName через Edu_Students → Speciality → OrgUnit
        var ssoStudentsRaw = await _ssoContext.Edu_Students
            .AsNoTracking()
            .Where(s => ssoStudentIdToInternal.Keys.Contains(s.StudentID))
            .Select(s => new
            {
                s.StudentID,
                s.SpecialityID,
                s.EducationPaymentTypeID,
                s.GrantTypeID,
                SpecRupEditorOrgUnitID = s.Speciality != null ? s.Speciality.RupEditorOrgUnitID : null,
                OrgUnitParentID = s.Speciality != null && s.Speciality.RupEditorOrgUnit != null
                    ? s.Speciality.RupEditorOrgUnit.ParentID : null,
                OrgUnitParentTitle = s.Speciality != null && s.Speciality.RupEditorOrgUnit != null
                    && s.Speciality.RupEditorOrgUnit.Parent != null
                    && s.Speciality.RupEditorOrgUnit.Parent.TypeID == 2
                        ? s.Speciality.RupEditorOrgUnit.Parent.Title
                        : null,
            })
            .ToListAsync(ct);

        var ssoExtraDict = ssoStudentsRaw.ToDictionary(x => x.StudentID);

        // 2c. Грант-типы из ССО для точного вычисления GrantType
        var ssoGrantTypes = await _ssoContext.Edu_GrantTypes
            .AsNoTracking()
            .Where(g => g.ESUVOGrantTypeId != null)
            .ToDictionaryAsync(g => g.ID, g => g.ESUVOGrantTypeId, ct);

        var ssoEptTypes = await _ssoContext.Edu_EducationPaymentTypes
            .AsNoTracking()
            .Where(e => e.ESUVOGrantTypeId != null)
            .ToDictionaryAsync(e => e.ID, e => e.ESUVOGrantTypeId, ct);

        // ── 3. Загружаем справочники ЕПВО ──
        var studyForms = await _epvoContext.Study_forms
            .AsNoTracking()
            .Where(x => x.NameRu != null && x.Id != null)
            .ToDictionaryAsync(x => x.Id!.Value, x => x.NameRu!, ct);

        var faculties = await _epvoContext.Faculties
            .AsNoTracking()
            .Where(x => x.FacultyNameRu != null)
            .ToDictionaryAsync(x => x.FacultyId, x => x.FacultyNameRu!, ct);

        var specialities = await _epvoContext.SpecialitiesEpvoNew
            .AsNoTracking()
            .Where(x => x.NameRu != null && x.Id != null)
            .ToDictionaryAsync(
                x => x.Id!,
                x => $"{x.SpecializationCode ?? ""} {x.NameRu}".Trim(),
                ct);

        // ── 4. Загружаем текущие данные ЕПВО (DUMP или SSO) + STUDENT_INFO ──
        var epvoRows = await LoadEpvoDataAsync(ct);
        var epvoByStudentId = epvoRows.ToDictionary(x => x.StudentId);

        // ── 5. Проверяем наличие в STUDENT_TEMP ──
        var tempIds = await _epvoContext.Student_Temp
            .AsNoTracking()
            .Where(t => studentIds.Contains(t.StudentId))
            .Select(t => t.StudentId)
            .ToListAsync(ct);

        var tempByStudentId = tempIds.ToHashSet();

        // ── 6. Сравниваем и формируем результат ──
        var items = new List<SyncPreviewComparisonDto>();

        foreach (var sso in ssoTemps)
        {
            if (string.IsNullOrWhiteSpace(sso.IinPlt)) continue;

            // Дополняем SSO данные
            var internalId = sso.StudentId >= 60000000 ? sso.StudentId - 60000000 : 0;
            var extra = internalId > 0 && ssoExtraDict.TryGetValue(internalId, out var extraTmp) ? extraTmp : null;
            if (extra != null)
            {
                // FacultyId
                if (extra.OrgUnitParentID.HasValue)
                    sso.FacultyId = extra.OrgUnitParentID.Value;
            }

            // ИИК / БИК / UpdatedDate из Scollarship_Students_Info
            DateTime? ssoUpdated = null;
            if (internalId > 0 && ssiDict.TryGetValue(internalId, out var ssi))
            {
                sso.Iic = ssi.Iic;
                sso.Bic = ssi.Bic;
                ssoUpdated = ssi.Updated_Date;
            }

            // GrantType по логике Zapros.txt
            int? eptId = extra?.EducationPaymentTypeID;
            int? gtId = extra?.GrantTypeID;
            int? epvoGrantType = ComputeGrantType(
                eptId.HasValue && ssoEptTypes.TryGetValue(eptId.Value, out var eptVal) ? eptVal : null,
                gtId.HasValue && ssoGrantTypes.TryGetValue(gtId.Value, out var gtVal) ? gtVal : null);
            sso.GrantType = epvoGrantType;

            epvoByStudentId.TryGetValue(sso.StudentId, out var epvo);
            bool isNew = epvo == null;

            var diffs = new List<FieldDiffDto>();

            if (!isNew)
            {
                diffs.AddRange(CompareFields(sso, epvo!, studyForms, faculties, specialities));
                if (diffs.Count == 0) continue; // совпадает — пропускаем
            }

            var fullName = $"{sso.LastName} {sso.FirstName} {sso.Patronymic}".Trim();
            faculties.TryGetValue(sso.FacultyId ?? 0, out var facultyName);
            specialities.TryGetValue(sso.SpecializationId?.ToString() ?? "0", out var specializationName);
            var studyFormName = sso.StudyFormId.HasValue && studyForms.TryGetValue(sso.StudyFormId.Value, out var sf) ? sf : null;
            var paymentType = sso.PaymentFormId == 2 ? "Стипендия" : sso.PaymentFormId == 1 ? "Платник" : null;
            var grantTypeLabel = ResolveGrantTypeLabel(sso.GrantType);

            items.Add(new SyncPreviewComparisonDto
            {
                StudentId = sso.StudentId,
                IinPlt = sso.IinPlt,
                FullName = fullName,
                CourseNumber = sso.CourseNumber,
                StudyForm = studyFormName,
                FacultyName = facultyName,
                Specialization = specializationName,
                PaymentType = paymentType,
                GrantType = grantTypeLabel,
                Iic = sso.Iic,
                Bic = sso.Bic,
                SsoUpdatedDate = ssoUpdated,
                EpvoUpdateDate = epvo?.UpdateDate,
                IsNew = isNew,
                HasDifference = diffs.Count > 0,
                DifferentFields = diffs.Select(d => d.FieldName).ToList(),
                FieldDiffs = diffs,
                IsInTemp = tempByStudentId.Contains(sso.StudentId),
                TempData = MapToDto(sso)
            });
        }

        // ── 7. Фильтрация, поиск, пагинация ──
        var filtered = items.AsEnumerable();

        if (filter == "diff")
            filtered = filtered.Where(i => !i.IsNew && i.HasDifference);
        else if (filter == "new")
            filtered = filtered.Where(i => i.IsNew);
        else if (filter == "temp")
            filtered = filtered.Where(i => i.IsInTemp);
        else if (filter == "not-temp")
            filtered = filtered.Where(i => !i.IsInTemp);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var q = search.Trim().ToLowerInvariant();
            filtered = filtered.Where(s =>
                (s.IinPlt != null && s.IinPlt.Contains(q, StringComparison.OrdinalIgnoreCase)) ||
                (s.FullName != null && s.FullName.Contains(q, StringComparison.OrdinalIgnoreCase)));
        }

        var sorted = filtered
            .OrderBy(s => s.FullName ?? "", StringComparer.Create(new System.Globalization.CultureInfo("ru-RU"), ignoreCase: true))
            .ToList();

        var filteredCount = sorted.Count;
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 200);
        var totalPages = Math.Max(1, (int)Math.Ceiling(filteredCount / (double)pageSize));
        page = Math.Min(page, totalPages);

        var pageItems = sorted
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new SyncPreviewComparisonPagedDto
        {
            Items = pageItems,
            TotalItems = items.Count,
            DiffCount = items.Count(x => !x.IsNew),
            NewCount = items.Count(x => x.IsNew),
            FilteredCount = filteredCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages
        };
    }

    public async Task<int> SaveToTempAsync(
        List<EpvoStudentTempDto> items,
        string sessionId,
        CancellationToken ct = default)
    {
        if (items == null || !items.Any()) return 0;

        var entities = items.Select(dto => new Student_Temp
        {
            UniversityId = dto.UniversityId,
            StudentId = dto.StudentId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Patronymic = dto.Patronymic,
            BirthDate = dto.BirthDate,
            StartDate = dto.StartDate,
            Address = dto.Address,
            NationId = dto.NationId,
            StudyFormId = dto.StudyFormId,
            StudyCalendarId = dto.StudyCalendarId,
            PaymentFormId = dto.PaymentFormId,
            StudyLanguageId = dto.StudyLanguageId,
            Photo = dto.Photo,
            ProfessionId = dto.ProfessionId,
            CourseNumber = dto.CourseNumber,
            TranscriptNumber = dto.TranscriptNumber,
            TranscriptSeries = dto.TranscriptSeries,
            IsMarried = dto.IsMarried,
            IcNumber = dto.IcNumber,
            IcDate = dto.IcDate,
            Education = dto.Education,
            HasExcellent = dto.HasExcellent,
            StartOrder = dto.StartOrder,
            IsStudent = dto.IsStudent,
            Certificate = dto.Certificate,
            GrantNumber = dto.GrantNumber,
            Gpa = dto.Gpa,
            CurrentCreditsSum = dto.CurrentCreditsSum,
            Residence = dto.Residence,
            SitizenshipId = dto.SitizenshipId,
            DormState = dto.DormState,
            IsInRetire = dto.IsInRetire,
            FromId = dto.FromId,
            Local = dto.Local,
            City = dto.City,
            ContractId = dto.ContractId,
            SpecializationId = dto.SpecializationId,
            IinPlt = dto.IinPlt,
            AltynBelgi = dto.AltynBelgi,
            DataVydachiAttestata = dto.DataVydachiAttestata,
            DataVydachiDiploma = dto.DataVydachiDiploma,
            DateDocEducation = dto.DateDocEducation,
            EndCollege = dto.EndCollege,
            EndHighSchool = dto.EndHighSchool,
            EndSchool = dto.EndSchool,
            IcSeries = dto.IcSeries,
            IcType = dto.IcType,
            LivingAddress = dto.LivingAddress,
            NomerAttestata = dto.NomerAttestata,
            OtherBirthPlace = dto.OtherBirthPlace,
            SeriesNumberDocEducation = dto.SeriesNumberDocEducation,
            SeriyaAttestata = dto.SeriyaAttestata,
            SeriyaDiploma = dto.SeriyaDiploma,
            SchoolName = dto.SchoolName,
            FacultyId = dto.FacultyId,
            SexId = dto.SexId,
            Mail = dto.Mail,
            Phone = dto.Phone,
            SumPoints = dto.SumPoints,
            SumPointsCreative = dto.SumPointsCreative,
            EnrollOrderDate = dto.EnrollOrderDate,
            MobilePhone = dto.MobilePhone,
            GrantType = dto.GrantType,
            AcademicMobility = dto.AcademicMobility,
            IncorrectIin = dto.IncorrectIin,
            BirthPlaceCatoId = dto.BirthPlaceCatoId,
            LivingPlaceCatoId = dto.LivingPlaceCatoId,
            RegistrationPlaceCatoId = dto.RegistrationPlaceCatoId,
            NaselennyiPunktAttestataCatoId = dto.NaselennyiPunktAttestataCatoId,
            FundingId = dto.FundingId,
            TypeCode = dto.TypeCode,
            Iic = dto.Iic,
            Bic = dto.Bic,
            BankId = dto.BankId,
            UpdateDate = dto.UpdateDate
        }).ToList();

        // Upsert: обновляем существующие, добавляем новые
        var ids = entities.Select(e => e.StudentId).ToHashSet();
        var existing = await _epvoContext.Student_Temp
            .Where(t => ids.Contains(t.StudentId))
            .ToListAsync(ct);

        var existingDict = existing.ToDictionary(e => e.StudentId);

        foreach (var entity in entities)
        {
            if (existingDict.TryGetValue(entity.StudentId, out var exist))
            {
                // Обновляем все поля
                _epvoContext.Entry(exist).CurrentValues.SetValues(entity);
            }
            else
            {
                _epvoContext.Student_Temp.Add(entity);
            }
        }

        await _epvoContext.SaveChangesAsync(ct);
        return entities.Count;
    }

    #region EPVO Data Loading

    private async Task<List<EpvoRow>> LoadEpvoDataAsync(CancellationToken ct)
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

        return await _epvoContext.Database
            .SqlQueryRaw<EpvoRow>(sql)
            .ToListAsync(ct);
    }

    private sealed class EpvoRow
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

    private static List<FieldDiffDto> CompareFields(
        Student_Temp sso,
        EpvoRow epvo,
        Dictionary<int, string> studyForms,
        Dictionary<int, string> faculties,
        Dictionary<string, string> specialities)
    {
        var diffs = new List<FieldDiffDto>();

        // ФИО
        var ssoFio = $"{sso.LastName} {sso.FirstName} {sso.Patronymic}".Trim();
        var epvoFio = $"{epvo.LastName} {epvo.FirstName} {epvo.Patronymic}".Trim();
        if (!EqualNormalized(ssoFio, epvoFio))
            diffs.Add(new FieldDiffDto { FieldName = "ФИО", SsoValue = ssoFio, EpvoValue = epvoFio });

        // Курс
        if (!EqualNormalized(sso.CourseNumber?.ToString(), epvo.CourseNumber?.ToString()))
            diffs.Add(new FieldDiffDto { FieldName = "Курс", SsoValue = sso.CourseNumber?.ToString(), EpvoValue = epvo.CourseNumber?.ToString() });

        // Форма обучения
        var ssoSf = sso.StudyFormId.HasValue && studyForms.TryGetValue(sso.StudyFormId.Value, out var s1) ? s1 : null;
        var epvoSf = epvo.StudyFormId.HasValue && studyForms.TryGetValue(epvo.StudyFormId.Value, out var s2) ? s2 : null;
        if (!EqualNormalized(ssoSf, epvoSf))
            diffs.Add(new FieldDiffDto { FieldName = "Форма обучения", SsoValue = ssoSf, EpvoValue = epvoSf });

        // Институт
        var ssoFac = sso.FacultyId.HasValue && faculties.TryGetValue(sso.FacultyId.Value, out var f1) ? f1 : null;
        var epvoFac = epvo.FacultyId.HasValue && faculties.TryGetValue(epvo.FacultyId.Value, out var f2) ? f2 : null;
        if (!EqualNormalized(ssoFac, epvoFac))
            diffs.Add(new FieldDiffDto { FieldName = "Институт", SsoValue = ssoFac, EpvoValue = epvoFac });

        // Специализация
        var ssoSpec = sso.SpecializationId.HasValue && specialities.TryGetValue(sso.SpecializationId.Value.ToString(), out var sp1) ? sp1 : null;
        var epvoSpec = epvo.SpecializationId.HasValue && specialities.TryGetValue(epvo.SpecializationId.Value.ToString(), out var sp2) ? sp2 : null;
        if (!EqualNormalized(ssoSpec, epvoSpec))
            diffs.Add(new FieldDiffDto { FieldName = "Специализация", SsoValue = ssoSpec, EpvoValue = epvoSpec });

        // Тип оплаты
        var ssoPay = sso.PaymentFormId == 2 ? "Стипендия" : sso.PaymentFormId == 1 ? "Платник" : null;
        var epvoPay = epvo.PaymentFormId == 2 ? "Стипендия" : epvo.PaymentFormId == 1 ? "Платник" : null;
        if (!EqualNormalized(ssoPay, epvoPay))
            diffs.Add(new FieldDiffDto { FieldName = "Тип оплаты", SsoValue = ssoPay, EpvoValue = epvoPay });

        // Тип гранта
        var ssoGrant = ResolveGrantTypeLabel(sso.GrantType);
        var epvoGrant = ResolveGrantTypeLabel(epvo.GrantType);
        if (!EqualNormalized(ssoGrant, epvoGrant))
            diffs.Add(new FieldDiffDto { FieldName = "Тип гранта", SsoValue = ssoGrant, EpvoValue = epvoGrant });

        // ИИК
        if (!EqualNormalized(sso.Iic, epvo.Iic))
            diffs.Add(new FieldDiffDto { FieldName = "ИИК (Р/С)", SsoValue = sso.Iic, EpvoValue = epvo.Iic });

        // БИК
        if (!EqualNormalized(sso.Bic, epvo.Bic))
            diffs.Add(new FieldDiffDto { FieldName = "БИК", SsoValue = sso.Bic, EpvoValue = epvo.Bic });

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

    #region Grant Type Logic (from Zapros.txt)

    /// <summary>
    /// ISNULL(IIF(ept.ESUVOGrantTypeId = 1, gtype.ESUVOGrantTypeId, ept.ESUVOGrantTypeId), 0)
    /// </summary>
    private static int? ComputeGrantType(int? eptESUVOGrantTypeId, int? gtypeESUVOGrantTypeId)
    {
        if (eptESUVOGrantTypeId == 1)
            return gtypeESUVOGrantTypeId ?? 0;
        return eptESUVOGrantTypeId ?? 0;
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
            StudyCalendarId = src.StudyCalendarId,
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
            TypeCode = src.TypeCode,
            Iic = src.Iic,
            Bic = src.Bic,
            BankId = src.BankId,
            UpdateDate = src.UpdateDate
        };
    }

    #endregion
}
