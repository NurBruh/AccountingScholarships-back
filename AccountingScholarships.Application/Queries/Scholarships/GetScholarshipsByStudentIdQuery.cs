
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.Scholarships;

public record GetScholarshipsByStudentIdQuery(int StudentId) : IRequest<IReadOnlyList<ScholarshipDto>>;
