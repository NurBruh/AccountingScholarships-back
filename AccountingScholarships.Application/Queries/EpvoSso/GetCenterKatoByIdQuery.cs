using AccountingScholarships.Domain.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetCenterKatoByIdQuery(int Id) : IRequest<CenterKatoDto?>;
