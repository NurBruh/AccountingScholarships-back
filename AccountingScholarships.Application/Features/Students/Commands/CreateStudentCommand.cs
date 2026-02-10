using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Students.Commands;

public record CreateStudentCommand(CreateStudentDto Student) : IRequest<StudentDto>;
