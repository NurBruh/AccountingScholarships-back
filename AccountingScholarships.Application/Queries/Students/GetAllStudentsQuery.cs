
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.Students;

public record GetAllStudentsQuery : IRequest<IReadOnlyList<StudentDto>>;
