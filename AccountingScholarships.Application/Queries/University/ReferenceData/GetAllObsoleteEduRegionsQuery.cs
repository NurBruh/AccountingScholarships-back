using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllObsoleteEduRegionsQuery : IRequest<IReadOnlyList<Obsolete_Edu_RegionsDto>>;
