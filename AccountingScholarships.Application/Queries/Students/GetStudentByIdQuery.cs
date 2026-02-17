
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.Students;

public record GetStudentByIdQuery(int Id) : IRequest<StudentDto?>;
