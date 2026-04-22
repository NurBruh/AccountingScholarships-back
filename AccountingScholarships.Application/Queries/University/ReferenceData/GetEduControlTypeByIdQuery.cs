using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduControlTypeByIdQuery(int Id) : IRequest<Edu_ControlTypesDto?>;
