
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Commands.Grants;

public record CreateGrantCommand(CreateGrantDto Dto) : IRequest<GrantDto>;
