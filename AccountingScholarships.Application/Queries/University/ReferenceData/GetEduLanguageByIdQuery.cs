using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduLanguageByIdQuery(int Id) : IRequest<Edu_LanguagesDto?>;
