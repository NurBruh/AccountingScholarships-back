using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Scholarships.Queries;

public record GetScholarshipByIdQuery(Guid Id) : IRequest<ScholarshipDto?>;
