
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Commands.Students;

public record CreateStudentCommand(CreateStudentDto Student) : IRequest<StudentDto>;
