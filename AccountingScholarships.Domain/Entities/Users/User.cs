using AccountingScholarships.Domain.Entities.Auth;

namespace AccountingScholarships.Domain.Entities.Users;

using AccountingScholarships.Domain.Common;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "User";

    public ICollection<UserRoleAssignment> UserAssignments { get; set; } = new List<UserRoleAssignment>();
}
