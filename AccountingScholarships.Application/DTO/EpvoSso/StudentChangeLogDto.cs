namespace AccountingScholarships.Application.DTO.EpvoSso;

public class StudentChangeLogDto
{
    public long Id { get; set; }
    public string? IinPlt { get; set; }
    public string FieldName { get; set; } = "";
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public string? DataSource { get; set; }
    public DateTime ChangedAt { get; set; }
    public string? ChangedBy { get; set; }
    public string? SyncSessionId { get; set; }
}
