using AccountingScholarships.Domain.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetAllSpecialitiesEpvo2025Query : IRequest<IReadOnlyList<SpecialitiesEpvo2025Dto>>;
