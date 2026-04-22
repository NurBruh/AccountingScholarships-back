using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public record GetEduPositionByIdQuery(int Id) : IRequest<Edu_PositionsDto?>;
