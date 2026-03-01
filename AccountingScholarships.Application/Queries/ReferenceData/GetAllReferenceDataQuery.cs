using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.ReferenceData;

public record GetAllReferenceDataQuery : IRequest<ReferenceDataDto>;
