using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Scholarships.Commands;

public record UpdateScholarshipCommand(Guid Id, UpdateScholarshipDto Dto) : IRequest<ScholarshipDto?>;
