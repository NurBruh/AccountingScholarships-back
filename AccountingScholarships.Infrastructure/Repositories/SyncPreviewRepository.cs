using AccountingScholarships.Application.DTO;
using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Infrastructure.Data;
using AccountingScholarships.Infrastructure.Services.StudentSync;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

/// <summary>
/// Репозиторий предпросмотра синхронизации.
/// Использует данные ComparisonRepository (ССО vs ЕПВО) и добавляет:
/// — статус наличия в STUDENT_TEMP
/// — пагинацию, фильтрацию, поиск
/// — сохранение в TEMP через трансформацию SSO → ЕПВО
/// </summary>
public class SyncPreviewRepository : ISyncPreviewRepository
{
    private readonly IComparisonRepository _comparisonRepo;
    private readonly ISsoToEpvoMapperService _mapper;
    private readonly EpvoSsoDbContext _epvoContext;

    public SyncPreviewRepository(
        IComparisonRepository comparisonRepo,
        ISsoToEpvoMapperService mapper,
        EpvoSsoDbContext epvoContext)
    {
        _comparisonRepo = comparisonRepo;
        _mapper = mapper;
        _epvoContext = epvoContext;
    }

    public async Task<SyncPreviewComparisonPagedDto> GetPreviewAsync(
        int page,
        int pageSize,
        string filter,
        string? search,
        CancellationToken ct = default)
    {
        // ── 1. Получаем данные сравнения (все записи) ──
        var comparison = await _comparisonRepo.GetComparisonAsync(ct);

        // ── 2. Фильтруем: только различия, убираем "только в ЕПВО" ──
        var filtered = comparison
            .Where(c => c.HasDifference)
            .Where(c => !c.DifferentFields.Contains("Нет в ССО"))
            .ToList();

        // ── 3. Проверяем наличие в STUDENT_TEMP ──
        var studentIds = filtered.Select(c => c.StudentId).ToHashSet();
        var tempIds = await _epvoContext.Student_Temp
            .AsNoTracking()
            .Where(t => studentIds.Contains(t.StudentId))
            .Select(t => t.StudentId)
            .ToListAsync(ct);

        var tempByStudentId = tempIds.ToHashSet();

        // ── 4. Маппим в DTO предпросмотра ──
        var items = filtered.Select(c => new SyncPreviewComparisonDto
        {
            StudentId = c.StudentId,
            IinPlt = c.IIN,
            FullName = c.Sso_FullName ?? c.Epvo_FullName,
            CourseNumber = c.Sso_CourseNumber ?? c.Epvo_CourseNumber,
            StudyForm = c.Sso_StudyForm ?? c.Epvo_StudyForm,
            FacultyName = c.Sso_Institute ?? c.Epvo_FacultyName,
            Specialization = c.Sso_Speciality ?? c.Epvo_Specialization,
            PaymentType = c.Sso_PaymentType ?? c.Epvo_PaymentType,
            GrantType = c.Sso_GrantType ?? c.Epvo_GrantType,
            Iic = c.Sso_Iic,
            Bic = c.Sso_Bic,
            SsoUpdatedDate = c.Sso_UpdatedDate,
            EpvoUpdateDate = c.Epvo_UpdateDate,
            IsNew = c.DifferentFields.Contains("Нет в ЕПВО"),
            HasDifference = c.HasDifference,
            DifferentFields = c.DifferentFields.Where(f => f != "Нет в ЕПВО").ToList(),
            FieldDiffs = MapFieldDiffs(c),
            IsInTemp = tempByStudentId.Contains(c.StudentId)
        }).ToList();

        // ── 5. Фильтрация по filter ──
        if (filter == "diff")
            items = items.Where(i => !i.IsNew && i.HasDifference).ToList();
        else if (filter == "new")
            items = items.Where(i => i.IsNew).ToList();
        else if (filter == "temp")
            items = items.Where(i => i.IsInTemp).ToList();
        else if (filter == "not-temp")
            items = items.Where(i => !i.IsInTemp).ToList();

        // ── 6. Поиск ──
        if (!string.IsNullOrWhiteSpace(search))
        {
            var q = search.Trim().ToLowerInvariant();
            items = items.Where(s =>
                (s.IinPlt != null && s.IinPlt.Contains(q, StringComparison.OrdinalIgnoreCase)) ||
                (s.FullName != null && s.FullName.Contains(q, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        // ── 7. Сортировка и пагинация ──
        var sorted = items
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
        List<string> iins,
        CancellationToken ct = default)
    {
        if (iins == null || !iins.Any()) return 0;

        // 1. Трансформируем ССО → ЕПВО формат
        var mapped = await _mapper.MapStudentsAsync(iins, ct);

        // 2. Дополняем ИИК/БИК из Scollarship_Students_Info (через SSO контекст)
        var internalIds = mapped
            .Where(m => m.StudentId >= 60000000)
            .Select(m => m.StudentId - 60000000)
            .ToHashSet();

        // 3. Upsert в STUDENT_TEMP
        var ids = mapped.Select(m => m.StudentId).ToHashSet();
        var existing = await _epvoContext.Student_Temp
            .Where(t => ids.Contains(t.StudentId))
            .ToListAsync(ct);

        var existingDict = existing.ToDictionary(e => e.StudentId);

        foreach (var entity in mapped)
        {
            if (existingDict.TryGetValue(entity.StudentId, out var exist))
            {
                _epvoContext.Entry(exist).CurrentValues.SetValues(entity);
            }
            else
            {
                _epvoContext.Student_Temp.Add(entity);
            }
        }

        await _epvoContext.SaveChangesAsync(ct);
        return mapped.Count;
    }

    #region Mapping Helpers

    private static List<FieldDiffDto> MapFieldDiffs(StudentComparisonDto dto)
    {
        var diffs = new List<FieldDiffDto>();
        foreach (var field in dto.DifferentFields)
        {
            if (field == "Нет в ЕПВО" || field == "Нет в ССО") continue;

            var diff = field switch
            {
                "ФИО" => new FieldDiffDto
                {
                    FieldName = "ФИО",
                    SsoValue = dto.Sso_FullName,
                    EpvoValue = dto.Epvo_FullName
                },
                "Курс" => new FieldDiffDto
                {
                    FieldName = "Курс",
                    SsoValue = dto.Sso_CourseNumber?.ToString(),
                    EpvoValue = dto.Epvo_CourseNumber?.ToString()
                },
                "Форма обучения" => new FieldDiffDto
                {
                    FieldName = "Форма обучения",
                    SsoValue = dto.Sso_StudyForm,
                    EpvoValue = dto.Epvo_StudyForm
                },
                "Институт" => new FieldDiffDto
                {
                    FieldName = "Институт",
                    SsoValue = dto.Sso_Institute,
                    EpvoValue = dto.Epvo_FacultyName
                },
                "Специализация" => new FieldDiffDto
                {
                    FieldName = "Специализация",
                    SsoValue = dto.Sso_Speciality,
                    EpvoValue = dto.Epvo_Specialization
                },
                "Тип оплаты" => new FieldDiffDto
                {
                    FieldName = "Тип оплаты",
                    SsoValue = dto.Sso_PaymentType,
                    EpvoValue = dto.Epvo_PaymentType
                },
                "Тип гранта" => new FieldDiffDto
                {
                    FieldName = "Тип гранта",
                    SsoValue = dto.Sso_GrantType,
                    EpvoValue = dto.Epvo_GrantType
                },
                "ИИК (Р/С)" => new FieldDiffDto
                {
                    FieldName = "ИИК (Р/С)",
                    SsoValue = dto.Sso_Iic,
                    EpvoValue = dto.Epvo_Iic
                },
                "БИК" => new FieldDiffDto
                {
                    FieldName = "БИК",
                    SsoValue = dto.Sso_Bic,
                    EpvoValue = dto.Epvo_Bic
                },
                _ => null
            };

            if (diff != null) diffs.Add(diff);
        }
        return diffs;
    }

    #endregion
}
