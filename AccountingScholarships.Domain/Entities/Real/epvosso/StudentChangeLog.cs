namespace AccountingScholarships.Domain.Entities.Real.epvosso;

/// <summary>
/// Лог изменений полей студента при синхронизации.
/// Фиксирует какое поле, у какого студента (ИИН), с какого на какое значение изменилось.
/// </summary>
public class StudentChangeLog
{
    public long Id { get; set; }

    /// <summary>ИИН студента</summary>
    public string? IinPlt { get; set; }

    /// <summary>Имя изменённого поля</summary>
    public string FieldName { get; set; } = "";

    /// <summary>Старое значение</summary>
    public string? OldValue { get; set; }

    /// <summary>Новое значение</summary>
    public string? NewValue { get; set; }

    /// <summary>Источник данных (SSO / EPVO)</summary>
    public string? DataSource { get; set; }

    /// <summary>Дата изменения (UTC)</summary>
    public DateTime ChangedAt { get; set; }

    /// <summary>Кто инициировал (имя из JWT)</summary>
    public string? ChangedBy { get; set; }

    /// <summary>ID сессии синхронизации (для группировки)</summary>
    public string? SyncSessionId { get; set; }
}
