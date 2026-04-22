using AccountingScholarships.Application.DTO.EpvoSso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public record GetEpvoSsoScholarshipByIdQuery(int Id) : IRequest<EpvoScholarshipSsoDto?>;
