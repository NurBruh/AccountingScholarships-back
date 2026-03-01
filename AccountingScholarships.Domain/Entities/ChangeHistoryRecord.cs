using AccountingScholarships.Domain.Common;

namespace AccountingScholarships.Domain.Entities;

/// <summary>
/// Запись об изменении данных студента (при синхронизации SSO↔EPVO).
/// </summary>
public class ChangeHistoryRecord : BaseEntity
{
    public string StudentIIN { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string FieldName { get; set; } = string.Empty;
    public string FieldLabel { get; set; } = string.Empty;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public string Source { get; set; } = string.Empty;
}
