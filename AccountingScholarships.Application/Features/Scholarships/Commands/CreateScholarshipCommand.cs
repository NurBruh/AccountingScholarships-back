using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Scholarships.Commands;

public record CreateScholarshipCommand(CreateScholarshipDto Dto) : IRequest<ScholarshipDto>;
