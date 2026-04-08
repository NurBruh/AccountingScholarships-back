namespace AccountingScholarships.Domain.Common;

public class SyncPreviewResult
{
    public int Total { get; set; }
    public int NewCount { get; set; }
    public int DuplicateCount { get; set; }
    public List<SyncPreviewItem> Items { get; set; } = new();
}

public class SyncPreviewItem
{
    public int? StudentId { get; set; }
    public string? IinPlt { get; set; }
    public string? FullName { get; set; }
    public int? CourseNumber { get; set; }
    public string? FacultyName { get; set; }
    public string? ProfessionName { get; set; }
    public string? PaymentType { get; set; }
    public string? GrantType { get; set; }
    public bool IsDuplicate { get; set; }
    public string? DuplicateSource { get; set; } // "STUDENT_SSO" | "STUDENT" | null
}
