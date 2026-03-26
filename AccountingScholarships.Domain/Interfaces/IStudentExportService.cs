using AccountingScholarships.Domain.DTO.Testing;

namespace AccountingScholarships.Domain.Interfaces;

/// <summary>
/// Интерфейс сервиса экспорта данных студентов в ЕСУ|ВО
/// </summary>
public interface IStudentExportService
{
    /// <summary>
    /// Получить данные студентов для экспорта в формат ЕСУ|ВО
    /// Выполняет сложный SQL запрос аналогичный хранимой процедуре Reload_STUDENT
    /// </summary>
    Task<IReadOnlyList<StudentEsuvoExportDto>> GetStudentsForExportAsync(CancellationToken cancellationToken = default);
}
