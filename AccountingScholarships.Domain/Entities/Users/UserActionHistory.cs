namespace AccountingScholarships.Domain.Entities.Users;

using AccountingScholarships.Domain.Common;

public class UserActionHistory : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string Action { get; set; } = string.Empty;
    public string? CreatedBy { get; set; }
}
