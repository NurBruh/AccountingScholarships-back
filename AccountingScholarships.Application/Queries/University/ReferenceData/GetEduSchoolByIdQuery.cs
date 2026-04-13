using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduSchoolByIdQuery(int Id) : IRequest<Edu_SchoolsDto?>;
