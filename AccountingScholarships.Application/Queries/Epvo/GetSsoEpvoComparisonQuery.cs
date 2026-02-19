using AccountingScholarships.Domain.DTO;
using MediatR;

namespace AccountingScholarships.Application.Queries.Epvo;

public class GetSsoEpvoComparisonQuery : IRequest<SsoEpvoComparisonDto> { }
