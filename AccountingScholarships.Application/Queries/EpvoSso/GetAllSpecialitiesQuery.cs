using AccountingScholarships.Application.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetAllSpecialitiesQuery : IRequest<IReadOnlyList<EpvoSpecialitiesDto>>;
