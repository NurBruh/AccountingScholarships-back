
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Commands.Students;

public record UpdateStudentCommand(Guid Id, UpdateStudentDto Student) : IRequest<StudentDto?>;
