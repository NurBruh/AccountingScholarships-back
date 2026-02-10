using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Grants.Commands;

public record CreateGrantCommand(CreateGrantDto Dto) : IRequest<GrantDto>;
