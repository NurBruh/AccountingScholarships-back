
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.Scholarships;

public record GetScholarshipsByStudentIdQuery(Guid StudentId) : IRequest<IReadOnlyList<ScholarshipDto>>;
