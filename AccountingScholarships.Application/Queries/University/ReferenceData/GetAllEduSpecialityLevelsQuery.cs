using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduSpecialityLevelsQuery : IRequest<IReadOnlyList<Edu_SpecialityLevelsDto>>;
