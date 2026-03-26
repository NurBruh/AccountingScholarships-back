using AccountingScholarships.Domain.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduSchoolByIdQuery(int Id) : IRequest<Edu_SchoolsDto?>;
