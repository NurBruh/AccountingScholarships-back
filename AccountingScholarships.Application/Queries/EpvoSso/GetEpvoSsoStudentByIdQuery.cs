using AccountingScholarships.Application.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetEpvoSsoStudentByIdQuery(int Id) : IRequest<EpvoStudentSsoDto?>;
