using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetAllEduMessengersQuery : IRequest<IReadOnlyList<Edu_MessengersDto>>;
