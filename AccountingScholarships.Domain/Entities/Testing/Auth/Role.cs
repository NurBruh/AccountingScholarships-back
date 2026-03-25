using System.Collections.Generic;

namespace AccountingScholarships.Domain.Entities.Testing.Auth;

/// <summary>
/// Справочник ролей
/// </summary>
public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // e.g. 'admin', 'teacher', 'department_head'
    public string? Description { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public ICollection<UserRoleAssignment> UserAssignments { get; set; } = new List<UserRoleAssignment>();
}
