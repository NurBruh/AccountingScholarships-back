using AccountingScholarships.Domain.Common;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Domain.Interfaces;
using AccountingScholarships.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AccountingScholarships.Infrastructure.Repositories;

/// <summary>
/// Вызов хранимых процедур через EpvoSsoDbContext (EPVO_test).
/// </summary>
public class StoredProcedureRepository : IStoredProcedureRepository
{
    private readonly EpvoSsoDbContext _context;

    public StoredProcedureRepository(EpvoSsoDbContext context)
    {
        _context = context;
    }

    public async Task<StoredProcedureResult> ExecuteReloadStudentAsync(CancellationToken ct = default)
    {
        // 1. Запоминаем ID-шки студентов ДО выполнения процедуры
        var existingIds = await _context.Student_Sso
            .AsNoTracking()
            .Select(s => s.StudentId)
            .ToListAsync(ct);

        var existingIdSet = new HashSet<int>(existingIds);

        // 2. Выполняем хранимую процедуру
        var returnValueParam = new SqlParameter
        {
            ParameterName = "@ReturnValue",
            SqlDbType = System.Data.SqlDbType.Int,
            Direction = System.Data.ParameterDirection.Output
        };

        var rowsAffected = await _context.Database
            .ExecuteSqlRawAsync(
                "EXEC @ReturnValue = [dbo].[Reload_STUDENT]",
                new[] { returnValueParam },
                ct);

        var returnValue = (int)returnValueParam.Value;

        // 3. Забираем НОВЫЕ записи (которых не было до выполнения)
        var insertedStudents = await _context.Student_Sso
            .AsNoTracking()
            .Where(s => !existingIdSet.Contains(s.StudentId))
            .ToListAsync(ct);

        return new StoredProcedureResult
        {
            ReturnValue = returnValue,
            RowsAffected = rowsAffected,
            ExecutedAt = DateTime.UtcNow,
            Message = returnValue == 0
                ? $"Успешно выполнено. Обработано записей: {rowsAffected}. Новых студентов: {insertedStudents.Count}."
                : $"Ошибка выполнения. Код возврата: {returnValue}.",
            InsertedStudents = insertedStudents
        };
    }

    public async Task<List<Student_Sso>> ReadReloadStudentAsync(CancellationToken ct = default)
    {
        var students = await _context.Student_Sso
            .FromSqlRaw("EXEC [dbo].[Reload_STUDENT]")
            .AsNoTracking()
            .ToListAsync(ct);

        return students;
    }
}
