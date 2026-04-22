using AccountingScholarships.Application.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetScholarshipNewByIdQuery(int Id) : IRequest<EpvoScholarshipNewDto?>;
