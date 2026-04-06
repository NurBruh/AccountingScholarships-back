using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetStudentComparisonQueryHandler
    : IRequestHandler<GetStudentComparisonQuery, StudentComparisonPagedDto>
{
    private readonly IComparisonRepository _repository;

    public GetStudentComparisonQueryHandler(IComparisonRepository repository)
    {
        _repository = repository;
    }

    public async Task<StudentComparisonPagedDto> Handle(
        GetStudentComparisonQuery request, CancellationToken cancellationToken)
    {
        var all = await _repository.GetComparisonAsync(cancellationToken);

        // Считаем статистику по ВСЕМ данным
        var totalItems = all.Count;
        var withDiff = all.Count(s => s.HasDifference
            && !s.DifferentFields.Contains("Нет в ССО")
            && !s.DifferentFields.Contains("Нет в ЕПВО"));
        var onlyInSso = all.Count(s => s.DifferentFields.Contains("Нет в ЕПВО"));
        var onlyInEpvo = all.Count(s => s.DifferentFields.Contains("Нет в ССО"));
        var matching = totalItems - withDiff - onlyInSso - onlyInEpvo;

        // Фильтрация
        IEnumerable<StudentComparisonDto> filtered = request.Filter switch
        {
            "diff" => all.Where(s => s.HasDifference
                && !s.DifferentFields.Contains("Нет в ССО")
                && !s.DifferentFields.Contains("Нет в ЕПВО")),
            "sso-only" => all.Where(s => s.DifferentFields.Contains("Нет в ЕПВО")),
            "epvo-only" => all.Where(s => s.DifferentFields.Contains("Нет в ССО")),
            "ok" => all.Where(s => !s.HasDifference),
            _ => all
        };

        // Поиск
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var q = request.Search.Trim().ToLowerInvariant();
            filtered = filtered.Where(s =>
                (s.IIN != null && s.IIN.Contains(q, StringComparison.OrdinalIgnoreCase)) ||
                (s.Sso_FullName != null && s.Sso_FullName.Contains(q, StringComparison.OrdinalIgnoreCase)) ||
                (s.Epvo_FullName != null && s.Epvo_FullName.Contains(q, StringComparison.OrdinalIgnoreCase)));
        }

        // Сортировка
        var sorted = filtered
            .OrderBy(s => s.Sso_FullName ?? s.Epvo_FullName ?? "",
                     StringComparer.Create(new System.Globalization.CultureInfo("ru-RU"), ignoreCase: true))
            .ToList();

        var filteredCount = sorted.Count;
        var page = Math.Max(1, request.Page);
        var pageSize = Math.Clamp(request.PageSize, 1, 200);
        var totalPages = Math.Max(1, (int)Math.Ceiling(filteredCount / (double)pageSize));
        page = Math.Min(page, totalPages);

        var pageItems = sorted
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new StudentComparisonPagedDto
        {
            Items = pageItems,
            TotalItems = totalItems,
            WithDifferences = withDiff,
            OnlyInSso = onlyInSso,
            OnlyInEpvo = onlyInEpvo,
            Matching = matching,
            FilteredCount = filteredCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages
        };
    }
}
