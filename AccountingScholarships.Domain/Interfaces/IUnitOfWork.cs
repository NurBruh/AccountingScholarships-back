using AccountingScholarships.Domain.Entities;
using AccountingScholarships.Domain.Entities.Auth;

namespace AccountingScholarships.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IStudentRepository Students { get; }
    IGrantRepository Grants { get; }
    IScholarshipRepository Scholarships { get; }
    IRepository<User> Users { get; }
    Task<UserRoleAssignment?> GetUserRoleAssignmentAsync(int userId, CancellationToken cancellationToken = default);
    Task<UserRoleAssignment> AssignRoleAsync(int userId, int roleId, string scopeType, int? scopeId, CancellationToken cancellationToken = default);
    Task RemoveRoleAssignmentAsync(int userId, CancellationToken cancellationToken = default);
    Task<List<(int Id, string Username, string Email, string Role, string? RoleName, string? ScopeType, int? ScopeId)>> GetAllUsersWithRolesAsync(CancellationToken cancellationToken = default);
    Task<string?> GetScopeNameAsync(string? scopeType, int? scopeId, CancellationToken cancellationToken = default);
    Task<List<string>> GetSpecialityNamesByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
