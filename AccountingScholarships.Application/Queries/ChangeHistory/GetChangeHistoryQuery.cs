using AccountingScholarships.Domain.Entities;
using MediatR;

namespace AccountingScholarships.Application.Queries.ChangeHistory;

public record GetChangeHistoryQuery(string? IIN = null) : IRequest<IReadOnlyList<ChangeHistoryRecord>>;
