
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.Scholarships;

public record GetScholarshipByIdQuery(Guid Id) : IRequest<ScholarshipDto?>;
