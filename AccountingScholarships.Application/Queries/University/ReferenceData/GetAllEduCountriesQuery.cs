using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetAllEduCountriesQuery : IRequest<IReadOnlyList<Edu_CountriesDto>>;
