using MediatR;

namespace AccountingScholarships.Application.Commands.Students;

public record DeleteStudentCommand(int Id) : IRequest<bool>;
