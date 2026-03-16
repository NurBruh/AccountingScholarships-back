using AccountingScholarships.Domain.Entities.Users;

namespace AccountingScholarships.Domain.Entities.Auth;

/// <summary>
/// Назначение роли пользователю с учетом контекста
/// </summary>
public class UserRoleAssignment
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

    /// <summary>
    /// Контекст назначения: 'global', 'institute', 'department', 'course'
    /// </summary>
    public string ScopeType { get; set; } = "global";

    /// <summary>
    /// ID конкретного объекта, к которому относится ScopeType. Если 'global', то NULL.
    /// </summary>
    public int? ScopeId { get; set; }
}
