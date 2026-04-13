using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public record GetAllEduSpecialitySpecializationsQuery : IRequest<IReadOnlyList<Edu_SpecialitySpecializationsDto>>;
