using System.Collections.Generic;

namespace AccountingScholarships.Domain.Entities.Testing.Auth;

/// <summary>
/// Справочник атомарных прав
/// </summary>
public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // e.g. 'edit_grades', 'view_reports'
    public string? Description { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
