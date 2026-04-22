using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduRupAlgorithmsQuery : IRequest<IReadOnlyList<Edu_RupAlgorithmsDto>>;
