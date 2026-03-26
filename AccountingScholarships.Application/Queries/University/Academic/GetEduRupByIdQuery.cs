using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public record GetEduRupByIdQuery(int Id) : IRequest<Edu_RupsDto?>;
