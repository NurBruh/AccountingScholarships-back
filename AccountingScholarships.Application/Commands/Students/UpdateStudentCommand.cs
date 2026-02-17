
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Commands.Students;

public record UpdateStudentCommand(int Id, UpdateStudentDto Student) : IRequest<StudentDto?>;
