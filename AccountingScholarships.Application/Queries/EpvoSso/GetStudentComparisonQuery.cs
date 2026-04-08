using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetStudentComparisonQuery : IRequest<StudentComparisonPagedDto>
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 50;
    public string Filter { get; init; } = "all";
    public string? Search { get; init; }
}
