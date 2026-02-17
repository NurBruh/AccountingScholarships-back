using MediatR;

namespace AccountingScholarships.Application.Commands.Epvo;

public record SyncStudentsFromEpvoCommand : IRequest<int>;
