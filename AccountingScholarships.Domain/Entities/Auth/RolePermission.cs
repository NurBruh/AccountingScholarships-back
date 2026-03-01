namespace AccountingScholarships.Domain.Entities.Auth;

/// <summary>
/// Связь ролей и прав
/// </summary>
public class RolePermission
{
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

    public int PermissionId { get; set; }
    public Permission Permission { get; set; } = null!;
}
