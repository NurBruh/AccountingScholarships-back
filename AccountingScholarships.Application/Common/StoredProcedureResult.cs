using AccountingScholarships.Domain.Entities.Real.epvosso;

namespace AccountingScholarships.Application.Common;

/// <summary>
/// Результат выполнения хранимой процедуры.
/// </summary>
public class StoredProcedureResult
{
    /// <summary>Код возврата процедуры (0 = успех).</summary>
    public int ReturnValue { get; set; }

    /// <summary>Количество затронутых строк (INSERT/UPDATE/DELETE).</summary>
    public int RowsAffected { get; set; }

    /// <summary>Время выполнения.</summary>
    public DateTime ExecutedAt { get; set; }

    /// <summary>Текстовое описание результата.</summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>Список студентов, которые были вставлены процедурой.</summary>
    public List<Student_Sso> InsertedStudents { get; set; } = new();
}
