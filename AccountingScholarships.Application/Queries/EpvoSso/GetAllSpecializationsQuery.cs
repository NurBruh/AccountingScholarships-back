using AccountingScholarships.Application.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetAllSpecializationsQuery : IRequest<IReadOnlyList<EpvoSpecializationsDto>>;
