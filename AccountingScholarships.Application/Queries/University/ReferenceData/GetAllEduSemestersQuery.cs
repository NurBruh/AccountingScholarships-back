using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduSemestersQuery : IRequest<IReadOnlyList<Edu_SemestersDto>>;
