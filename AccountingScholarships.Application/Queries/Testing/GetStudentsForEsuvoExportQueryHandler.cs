using AccountingScholarships.Domain.DTO.Testing;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.Testing;

/// <summary>
/// Handler для получения данных студентов для экспорта в ЕСУ|ВО
/// </summary>
public class GetStudentsForEsuvoExportQueryHandler 
    : IRequestHandler<GetStudentsForEsuvoExportQuery, IReadOnlyList<StudentEsuvoExportDto>>
{
    private readonly IStudentExportService _exportService;

    public GetStudentsForEsuvoExportQueryHandler(IStudentExportService exportService)
    {
        _exportService = exportService;
    }

    public async Task<IReadOnlyList<StudentEsuvoExportDto>> Handle(
        GetStudentsForEsuvoExportQuery request, 
        CancellationToken cancellationToken)
    {
        return await _exportService.GetStudentsForExportAsync(cancellationToken);
    }
}
