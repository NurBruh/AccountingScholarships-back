using AccountingScholarships.Application.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetCenterKatoByIdQuery(int Id) : IRequest<CenterKatoDto?>;
