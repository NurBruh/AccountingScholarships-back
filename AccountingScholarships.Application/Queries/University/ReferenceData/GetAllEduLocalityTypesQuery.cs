using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduLocalityTypesQuery : IRequest<IReadOnlyList<Edu_LocalityTypesDto>>;
