namespace AccountingScholarships.Domain.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(string userId, string username, string role, string? scopeType = null, int? scopeId = null);
}
