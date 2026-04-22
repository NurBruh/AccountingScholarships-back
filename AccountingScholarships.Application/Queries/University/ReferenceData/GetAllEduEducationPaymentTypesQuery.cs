using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetAllEduEducationPaymentTypesQuery : IRequest<IReadOnlyList<Edu_EducationPaymentTypesDto>>;
