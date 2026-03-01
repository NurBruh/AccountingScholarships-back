using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.ScholarshipLoss;

public record GetScholarshipLossRecordsQuery : IRequest<IReadOnlyList<ScholarshipLossRecordDto>>;
