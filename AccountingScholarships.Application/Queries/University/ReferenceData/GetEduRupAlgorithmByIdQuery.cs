using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetEduRupAlgorithmByIdQuery(int Id) : IRequest<Edu_RupAlgorithmsDto?>;
