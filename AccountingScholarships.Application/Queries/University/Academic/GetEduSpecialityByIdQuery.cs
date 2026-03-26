using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public record GetEduSpecialityByIdQuery(int Id) : IRequest<Edu_SpecialitiesDto?>;
