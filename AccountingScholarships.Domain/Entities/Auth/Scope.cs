namespace AccountingScholarships.Domain.Entities.Auth;

public class Scope
{
    public int Id { get; set; }
    public string ScopeName { get; set; } = string.Empty;
    public string? Description { get; set; }
}
