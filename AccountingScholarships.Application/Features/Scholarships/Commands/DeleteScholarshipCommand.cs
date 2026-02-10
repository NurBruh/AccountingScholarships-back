using MediatR;

namespace AccountingScholarships.Application.Features.Scholarships.Commands;

public record DeleteScholarshipCommand(Guid Id) : IRequest<bool>;
