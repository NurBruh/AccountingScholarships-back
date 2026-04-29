using AccountingScholarships.Application.DTO.EpvoSso;

namespace AccountingScholarships.Application.Common;

public class SyncPreviewResult
{
    public int Total { get; set; }
    public int DiffCount { get; set; }
    public int NewCount { get; set; }
    public List<SyncPreviewItem> Items { get; set; } = new();
}

public class SyncPreviewItem
{
    public int StudentId { get; set; }
    public string? IinPlt { get; set; }
    public string? FullName { get; set; }
    public int? CourseNumber { get; set; }
    public string? FacultyName { get; set; }
    public string? ProfessionName { get; set; }
    public string? PaymentType { get; set; }
    public string? GrantType { get; set; }
    public bool IsNew { get; set; }
    public List<string> DifferentFields { get; set; } = new();
    public List<FieldDiff> FieldDiffs { get; set; } = new();
    public EpvoStudentTempDto Data { get; set; } = new EpvoStudentTempDto();
}

public class FieldDiff
{
    public string FieldName { get; set; } = string.Empty;
    public string? SsoValue { get; set; }
    public string? EpvoValue { get; set; }
}
