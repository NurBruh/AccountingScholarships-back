using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public record GetAllEduRupsQuery : IRequest<IReadOnlyList<Edu_RupsDto>>;
