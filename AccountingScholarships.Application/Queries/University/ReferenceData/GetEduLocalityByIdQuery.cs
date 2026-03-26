using AccountingScholarships.Domain.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduLocalityByIdQuery(int Id) : IRequest<Edu_LocalitiesDto?>;
