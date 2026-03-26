using AccountingScholarships.Domain.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetAllEduEducationPaymentTypesQuery : IRequest<IReadOnlyList<Edu_EducationPaymentTypesDto>>;
