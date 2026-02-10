using MediatR;

namespace AccountingScholarships.Application.Features.Grants.Commands;

public record DeleteGrantCommand(Guid Id) : IRequest<bool>;
