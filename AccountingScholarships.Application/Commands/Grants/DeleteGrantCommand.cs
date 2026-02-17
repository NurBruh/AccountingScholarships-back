using MediatR;

namespace AccountingScholarships.Application.Commands.Grants;

public record DeleteGrantCommand(Guid Id) : IRequest<bool>;
