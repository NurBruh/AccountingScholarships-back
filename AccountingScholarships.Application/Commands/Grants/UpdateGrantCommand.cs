
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Commands.Grants;

public record UpdateGrantCommand(int Id, UpdateGrantDto Dto) : IRequest<GrantDto?>;
