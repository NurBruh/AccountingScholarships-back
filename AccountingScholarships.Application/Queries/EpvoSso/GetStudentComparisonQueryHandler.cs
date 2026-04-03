using AccountingScholarships.Domain.DTO;
using AccountingScholarships.Domain.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetStudentComparisonQueryHandler
    : IRequestHandler<GetStudentComparisonQuery, IReadOnlyList<StudentComparisonDto>>
{
    private readonly IComparisonRepository _repository;

    public GetStudentComparisonQueryHandler(IComparisonRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<StudentComparisonDto>> Handle(
        GetStudentComparisonQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetComparisonAsync(cancellationToken);
    }
}
