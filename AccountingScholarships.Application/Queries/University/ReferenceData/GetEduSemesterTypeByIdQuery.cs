using AccountingScholarships.Domain.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduSemesterTypeByIdQuery(int Id) : IRequest<Edu_SemesterTypesDto?>;
