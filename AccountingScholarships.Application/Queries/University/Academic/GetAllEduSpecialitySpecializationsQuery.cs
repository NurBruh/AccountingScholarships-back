using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public record GetAllEduSpecialitySpecializationsQuery : IRequest<IReadOnlyList<Edu_SpecialitySpecializationsDto>>;
