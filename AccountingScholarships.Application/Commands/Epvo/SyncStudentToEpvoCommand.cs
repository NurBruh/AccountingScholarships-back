using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

public record SyncStudentToEpvoCommand(string IIN) : IRequest<bool>;
