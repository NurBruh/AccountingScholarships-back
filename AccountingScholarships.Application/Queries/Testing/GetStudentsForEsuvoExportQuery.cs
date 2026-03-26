using AccountingScholarships.Domain.DTO.Testing;
using MediatR;

namespace AccountingScholarships.Application.Queries.Testing;

/// <summary>
/// Запрос на получение данных студентов для экспорта в ЕСУ|ВО
/// </summary>
public record GetStudentsForEsuvoExportQuery : IRequest<IReadOnlyList<StudentEsuvoExportDto>>;
