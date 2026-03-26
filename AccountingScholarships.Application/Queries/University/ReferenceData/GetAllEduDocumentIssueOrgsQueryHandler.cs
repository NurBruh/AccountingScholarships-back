using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetAllEduDocumentIssueOrgsQueryHandler : IRequestHandler<GetAllEduDocumentIssueOrgsQuery, IReadOnlyList<Edu_DocumentIssueOrgsDto>>
{
    private readonly ISsoRepository<Edu_DocumentIssueOrgs> _repository;
    public GetAllEduDocumentIssueOrgsQueryHandler(ISsoRepository<Edu_DocumentIssueOrgs> repository) { _repository = repository; }
    public async Task<IReadOnlyList<Edu_DocumentIssueOrgsDto>> Handle(GetAllEduDocumentIssueOrgsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new Edu_DocumentIssueOrgsDto { ID = e.ID, Title = e.Title }).ToList().AsReadOnly();
    }
}
