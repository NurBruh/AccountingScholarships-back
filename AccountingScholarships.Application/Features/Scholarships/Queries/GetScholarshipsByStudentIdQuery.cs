using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Scholarships.Queries;

public record GetScholarshipsByStudentIdQuery(Guid StudentId) : IRequest<IReadOnlyList<ScholarshipDto>>;
