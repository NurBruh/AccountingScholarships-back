using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduSpecializationsQuery : IRequest<IReadOnlyList<Edu_SpecializationsDto>>;
