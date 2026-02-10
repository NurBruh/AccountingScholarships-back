using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Students.Queries;

public record GetAllStudentsQuery : IRequest<IReadOnlyList<StudentDto>>;
