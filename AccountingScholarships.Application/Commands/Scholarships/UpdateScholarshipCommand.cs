
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Commands.Scholarships;

public record UpdateScholarshipCommand(int Id, UpdateScholarshipDto Dto) : IRequest<ScholarshipDto?>;
