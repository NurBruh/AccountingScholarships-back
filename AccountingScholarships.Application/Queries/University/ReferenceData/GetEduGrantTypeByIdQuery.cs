using AccountingScholarships.Domain.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduGrantTypeByIdQuery(int Id) : IRequest<Edu_GrantTypesDto?>;
