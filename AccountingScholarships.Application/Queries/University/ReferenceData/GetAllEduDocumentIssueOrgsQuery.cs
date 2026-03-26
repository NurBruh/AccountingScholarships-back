using AccountingScholarships.Domain.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetAllEduDocumentIssueOrgsQuery : IRequest<IReadOnlyList<Edu_DocumentIssueOrgsDto>>;
