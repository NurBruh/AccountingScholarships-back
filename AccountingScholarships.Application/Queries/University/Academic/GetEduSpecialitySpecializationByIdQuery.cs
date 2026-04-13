using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public record GetEduSpecialitySpecializationByIdQuery(int Id) : IRequest<Edu_SpecialitySpecializationsDto?>;
