using AccountingScholarships.Application.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetProfessionByIdQuery(int Id) : IRequest<EpvoProfessionDto?>;
