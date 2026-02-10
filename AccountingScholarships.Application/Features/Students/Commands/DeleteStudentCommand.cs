using MediatR;

namespace AccountingScholarships.Application.Features.Students.Commands;

public record DeleteStudentCommand(Guid Id) : IRequest<bool>;
