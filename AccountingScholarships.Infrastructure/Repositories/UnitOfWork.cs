using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Domain.Entities;
using AccountingScholarships.Infrastructure.Data;
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
