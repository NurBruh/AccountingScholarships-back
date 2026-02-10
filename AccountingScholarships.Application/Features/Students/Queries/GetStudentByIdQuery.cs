using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Students.Queries;

public record GetStudentByIdQuery(Guid Id) : IRequest<StudentDto?>;
