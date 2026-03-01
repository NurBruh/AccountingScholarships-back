namespace AccountingScholarships.Domain.Entities.Auth;

public class AccessRole
{
    public int Id { get; set; }
    public string AccessRoleName { get; set; } = string.Empty;
    public string? Description { get; set; }
}
