using AccountingScholarships.Domain.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetUniversityByIdQuery(int Id) : IRequest<EpvoUniversityDto?>;
