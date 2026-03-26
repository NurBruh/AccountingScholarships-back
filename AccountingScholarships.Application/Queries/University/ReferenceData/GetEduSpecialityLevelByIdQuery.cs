using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetEduSpecialityLevelByIdQuery(int Id) : IRequest<Edu_SpecialityLevelsDto?>;
