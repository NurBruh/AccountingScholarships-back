using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

public record SyncStudentsToEpvoCommand : IRequest<int>;
