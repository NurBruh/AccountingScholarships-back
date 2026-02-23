using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

public record UpdateStudentIbanCommand(string IIN, string NewIban) : IRequest<bool>;
