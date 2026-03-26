using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Domain.Interfaces;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public class GetEduDocumentIssueOrgByIdQueryHandler : IRequestHandler<GetEduDocumentIssueOrgByIdQuery, Edu_DocumentIssueOrgsDto?>
{
    private readonly ISsoRepository<Edu_DocumentIssueOrgs> _repository;
    public GetEduDocumentIssueOrgByIdQueryHandler(ISsoRepository<Edu_DocumentIssueOrgs> repository) { _repository = repository; }
    public async Task<Edu_DocumentIssueOrgsDto?> Handle(GetEduDocumentIssueOrgByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null) return null;
        return new Edu_DocumentIssueOrgsDto { ID = entity.ID, Title = entity.Title };
    }
}
