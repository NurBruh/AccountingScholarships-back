
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Commands.Grants;

public record UpdateGrantCommand(Guid Id, UpdateGrantDto Dto) : IRequest<GrantDto?>;
