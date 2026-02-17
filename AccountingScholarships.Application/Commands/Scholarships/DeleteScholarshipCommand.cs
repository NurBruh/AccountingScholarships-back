using MediatR;

namespace AccountingScholarships.Application.Commands.Scholarships;

public record DeleteScholarshipCommand(int Id) : IRequest<bool>;
