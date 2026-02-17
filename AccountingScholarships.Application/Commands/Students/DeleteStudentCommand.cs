using MediatR;

namespace AccountingScholarships.Application.Commands.Students;

public record DeleteStudentCommand(Guid Id) : IRequest<bool>;
