using AccountingScholarships.Application.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetMaritalstateByIdQuery(int Id) : IRequest<MaritalstatesDto?>;
