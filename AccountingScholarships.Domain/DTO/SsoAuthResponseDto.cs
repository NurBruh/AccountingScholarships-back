namespace AccountingScholarships.Domain.DTO;

public class SsoAuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string RoleDisplayName { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    
    /// <summary>
    /// Для директора института — ID оргюнита (института)
    /// </summary>
    public int? ScopeId { get; set; }
    
    /// <summary>
    /// Для директора института — название института
    /// </summary>
    public string? ScopeName { get; set; }
}
