
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Commands.Scholarships;

public record UpdateScholarshipCommand(Guid Id, UpdateScholarshipDto Dto) : IRequest<ScholarshipDto?>;
