using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduEntrantStatusByIdQuery(int Id) : IRequest<Edu_EntrantStatusesDto?>;
