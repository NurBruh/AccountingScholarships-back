namespace AccountingScholarships.Application.DTO;

/// <summary>
/// Элемент предпросмотра синхронизации ССО → ЕПВО.
/// Базируется на данных ComparisonRepository.
/// Показывает только различия и записи, отсутствующие в ЕПВО.
/// </summary>
public class SyncPreviewComparisonDto
{
    public int StudentId { get; set; }
    public string? IinPlt { get; set; }
    public string? FullName { get; set; }
    public int? CourseNumber { get; set; }
    public string? StudyForm { get; set; }
    public string? FacultyName { get; set; }
    public string? Specialization { get; set; }
    public string? PaymentType { get; set; }
    public string? GrantType { get; set; }
    public string? Iic { get; set; }
    public string? Bic { get; set; }
    public DateTime? SsoUpdatedDate { get; set; }
    public DateOnly? EpvoUpdateDate { get; set; }

    public bool IsNew { get; set; }
    public bool HasDifference { get; set; }
    public List<string> DifferentFields { get; set; } = new();
    public List<FieldDiffDto> FieldDiffs { get; set; } = new();

    /// <summary>true — запись уже есть в STUDENT_TEMP</summary>
    public bool IsInTemp { get; set; }
}

public class SyncPreviewComparisonPagedDto
{
    public List<SyncPreviewComparisonDto> Items { get; set; } = new();
    public int TotalItems { get; set; }
    public int DiffCount { get; set; }
    public int NewCount { get; set; }
    public int FilteredCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}

public class FieldDiffDto
{
    public string FieldName { get; set; } = string.Empty;
    public string? SsoValue { get; set; }
    public string? EpvoValue { get; set; }
}
