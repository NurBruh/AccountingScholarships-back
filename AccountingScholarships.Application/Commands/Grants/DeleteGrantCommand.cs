using MediatR;

namespace AccountingScholarships.Application.Commands.Grants;

public record DeleteGrantCommand(int Id) : IRequest<bool>;
