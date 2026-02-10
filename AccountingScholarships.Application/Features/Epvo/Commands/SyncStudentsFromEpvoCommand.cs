using MediatR;

namespace AccountingScholarships.Application.Features.Epvo.Commands;

public record SyncStudentsFromEpvoCommand : IRequest<int>;
