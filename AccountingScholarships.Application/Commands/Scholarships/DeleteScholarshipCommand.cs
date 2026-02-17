using MediatR;

namespace AccountingScholarships.Application.Commands.Scholarships;

public record DeleteScholarshipCommand(Guid Id) : IRequest<bool>;
