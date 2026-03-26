using AccountingScholarships.Domain.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduCountryByIdQuery(int Id) : IRequest<Edu_CountriesDto?>;
