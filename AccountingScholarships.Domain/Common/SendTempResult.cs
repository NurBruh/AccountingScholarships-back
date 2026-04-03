namespace AccountingScholarships.Domain.Common;

public class SendTempResult
{
    public int Total { get; set; }
    public int Success { get; set; }
    public int Errors { get; set; }
    public string Message { get; set; } = string.Empty;
}
