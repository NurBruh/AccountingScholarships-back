namespace AccountingScholarships.Domain.DTO;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public string? ScopeType { get; set; }
    public int? ScopeId { get; set; }
    public string? ScopeName { get; set; }
}
