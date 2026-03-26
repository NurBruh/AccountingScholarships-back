using AccountingScholarships.Domain.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetAllEduMaritalStatusesQuery : IRequest<IReadOnlyList<Edu_MaritalStatusesDto>>;
