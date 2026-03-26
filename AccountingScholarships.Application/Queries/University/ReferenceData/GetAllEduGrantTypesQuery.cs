using AccountingScholarships.Domain.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetAllEduGrantTypesQuery : IRequest<IReadOnlyList<Edu_GrantTypesDto>>;
