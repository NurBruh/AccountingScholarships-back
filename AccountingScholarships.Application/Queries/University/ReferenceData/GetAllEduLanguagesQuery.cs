using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetAllEduLanguagesQuery : IRequest<IReadOnlyList<Edu_LanguagesDto>>;
