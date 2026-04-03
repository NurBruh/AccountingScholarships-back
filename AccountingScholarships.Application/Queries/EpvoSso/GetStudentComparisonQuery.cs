using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetStudentComparisonQuery : IRequest<IReadOnlyList<StudentComparisonDto>>;
