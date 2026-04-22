using AccountingScholarships.Application.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetFacultyByIdQuery(int Id) : IRequest<FacultyDto?>;
