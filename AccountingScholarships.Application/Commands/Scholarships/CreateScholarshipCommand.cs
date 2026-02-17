
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Commands.Scholarships;

public record CreateScholarshipCommand(CreateScholarshipDto Dto) : IRequest<ScholarshipDto>;
