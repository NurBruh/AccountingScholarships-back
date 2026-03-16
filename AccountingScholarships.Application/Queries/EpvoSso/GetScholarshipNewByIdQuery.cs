using AccountingScholarships.Domain.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetScholarshipNewByIdQuery(int Id) : IRequest<EpvoScholarshipNewDto?>;
