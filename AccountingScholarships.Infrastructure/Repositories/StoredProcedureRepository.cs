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

    public async Task<List<Dictionary<string, object?>>> ReadReloadStudentAsync(CancellationToken ct = default)
    {
        var results = new List<Dictionary<string, object?>>();

        var connection = _context.Database.GetDbConnection();
        await connection.OpenAsync(ct);

        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = "EXEC [dbo].[Reload_STUDENT]";
            command.CommandTimeout = 120;

            using var reader = await command.ExecuteReaderAsync(ct);

            while (await reader.ReadAsync(ct))
            {
                var row = new Dictionary<string, object?>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }
                results.Add(row);
            }
        }
        finally
        {
            await connection.CloseAsync();
        }

        return results;
    }
}
