using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

public class ChangeLogRepository : IChangeLogRepository
{
    private readonly EpvoSsoDbContext _context;

    public ChangeLogRepository(EpvoSsoDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(IEnumerable<StudentChangeLog> changes, CancellationToken ct = default)
    {
        var list = changes.ToList();
        if (list.Count == 0) return;

        await _context.StudentChangeLogs.AddRangeAsync(list, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<ChangeLogPagedResult> GetChangeLogsAsync(string? iin, int page, int pageSize, CancellationToken ct = default)
    {
        var query = _context.StudentChangeLogs.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(iin))
            query = query.Where(x => x.IinPlt != null && x.IinPlt.Contains(iin));

        var total = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(x => x.ChangedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new ChangeLogPagedResult
        {
            Total = total,
            Page = page,
            PageSize = pageSize,
            Items = items
        };
    }

    public async Task<IReadOnlyList<StudentChangeLog>> GetChangeLogsByIinAsync(string iin, CancellationToken ct = default)
    {
        return await _context.StudentChangeLogs
            .AsNoTracking()
            .Where(x => x.IinPlt == iin)
            .OrderByDescending(x => x.ChangedAt)
            .ToListAsync(ct);
    }
}
