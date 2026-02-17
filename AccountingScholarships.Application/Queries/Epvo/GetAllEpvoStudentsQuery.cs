
using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.Epvo;

public record GetAllEpvoStudentsQuery : IRequest<IReadOnlyList<EpvoStudentDto>>;
