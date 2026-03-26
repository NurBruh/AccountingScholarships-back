using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduLocalitiesQuery : IRequest<IReadOnlyList<Edu_LocalitiesDto>>;
