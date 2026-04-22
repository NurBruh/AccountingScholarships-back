using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduSemestersQuery : IRequest<IReadOnlyList<Edu_SemestersDto>>;
