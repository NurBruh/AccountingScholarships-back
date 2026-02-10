using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Students.Commands;

public record UpdateStudentCommand(Guid Id, UpdateStudentDto Student) : IRequest<StudentDto?>;
