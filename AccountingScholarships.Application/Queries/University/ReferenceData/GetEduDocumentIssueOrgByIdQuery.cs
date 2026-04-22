using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduDocumentIssueOrgByIdQuery(int Id) : IRequest<Edu_DocumentIssueOrgsDto?>;
