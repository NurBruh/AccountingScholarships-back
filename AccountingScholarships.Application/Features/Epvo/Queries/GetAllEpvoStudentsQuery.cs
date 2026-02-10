using AccountingScholarships.Application.DTOs;
using MediatR;

namespace AccountingScholarships.Application.Features.Epvo.Queries;

public record GetAllEpvoStudentsQuery : IRequest<IReadOnlyList<EpvoStudentDto>>;
