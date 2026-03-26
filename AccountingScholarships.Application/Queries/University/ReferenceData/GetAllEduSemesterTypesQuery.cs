using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduSemesterTypesQuery : IRequest<IReadOnlyList<Edu_SemesterTypesDto>>;
