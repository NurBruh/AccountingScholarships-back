namespace AccountingScholarships.Domain.Entities.Testing.Scholarships;

using AccountingScholarships.Domain.Common;
using AccountingScholarships.Domain.Entities.Testing.Students;

/// <summary>
/// Запись о лишении стипендии (Сценарий 2 — отдельная таблица).
/// </summary>
public class ScholarshipLossRecord : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string IIN { get; set; } = string.Empty;
    public DateTime LostDate { get; set; }
    public string? OrderNumber { get; set; }
    public DateTime? OrderDate { get; set; }
    public string? Reason { get; set; }
    public string? ScholarshipName { get; set; }

    public int? StudentId { get; set; }
    public Student? Student { get; set; }
}
