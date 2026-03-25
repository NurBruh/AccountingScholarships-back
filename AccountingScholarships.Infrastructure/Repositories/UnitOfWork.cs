using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Domain.Entities.Testing.Users;
using AccountingScholarships.Domain.Entities.Testing.Auth;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AccountingScholarships.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;
    private IStudentRepository? _students;
    private IGrantRepository? _grants;
    private IScholarshipRepository? _scholarships;
    private IRepository<User>? _users;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IStudentRepository Students =>
        _students ??= new StudentRepository(_context);

    public IGrantRepository Grants =>
        _grants ??= new GrantRepository(_context);

    public IScholarshipRepository Scholarships =>
        _scholarships ??= new ScholarshipRepository(_context);

    public IRepository<User> Users =>
        _users ??= new Repository<User>(_context);

    public async Task<UserRoleAssignment?> GetUserRoleAssignmentAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.UserRoleAssignments
            .Include(a => a.Role)
            .FirstOrDefaultAsync(a => a.UserId == userId, cancellationToken);
    }

    public async Task<UserRoleAssignment> AssignRoleAsync(int userId, int roleId, string scopeType, int? scopeId, CancellationToken cancellationToken = default)
    {
        // Удаляем старое назначение если есть
        var existing = await _context.UserRoleAssignments
            .FirstOrDefaultAsync(a => a.UserId == userId, cancellationToken);
        if (existing != null)
            _context.UserRoleAssignments.Remove(existing);

        // Обновляем поле Role в User
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user != null && role != null)
            user.Role = role.Name;

        var assignment = new UserRoleAssignment
        {
            UserId = userId,
            RoleId = roleId,
            ScopeType = scopeType,
            ScopeId = scopeId
        };
        _context.UserRoleAssignments.Add(assignment);
        await _context.SaveChangesAsync(cancellationToken);
        
        assignment.Role = role!;
        return assignment;
    }

    public async Task RemoveRoleAssignmentAsync(int userId, CancellationToken cancellationToken = default)
    {
        var existing = await _context.UserRoleAssignments
            .FirstOrDefaultAsync(a => a.UserId == userId, cancellationToken);
        if (existing != null)
        {
            _context.UserRoleAssignments.Remove(existing);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            if (user != null)
                user.Role = "User";
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<(int Id, string Username, string Email, string Role, string? RoleName, string? ScopeType, int? ScopeId)>> GetAllUsersWithRolesAsync(CancellationToken cancellationToken = default)
    {
        var users = await _context.Users
            .Select(u => new
            {
                u.Id,
                u.Username,
                u.Email,
                u.Role,
                Assignment = _context.UserRoleAssignments
                    .Include(a => a.Role)
                    .FirstOrDefault(a => a.UserId == u.Id)
            })
            .ToListAsync(cancellationToken);

        return users.Select(u => (
            u.Id,
            u.Username,
            u.Email,
            u.Role,
            u.Assignment?.Role?.Name,
            u.Assignment?.ScopeType,
            u.Assignment?.ScopeId
        )).ToList();
    }

    public async Task<string?> GetScopeNameAsync(string? scopeType, int? scopeId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(scopeType) || !scopeId.HasValue)
            return null;

        if (scopeType == "department")
        {
            var dept = await _context.Departments.FirstOrDefaultAsync(d => d.Id == scopeId.Value, cancellationToken);
            return dept?.DepartmentName;
        }

        if (scopeType == "institute")
        {
            var inst = await _context.Institutes.FirstOrDefaultAsync(i => i.Id == scopeId.Value, cancellationToken);
            return inst?.InstituteName;
        }

        return null;
    }

    public async Task<List<string>> GetSpecialityNamesByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default)
    {
        return await _context.Specialities
            .Where(s => s.DepartmentId == departmentId)
            .Select(s => s.SpecialityName)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
