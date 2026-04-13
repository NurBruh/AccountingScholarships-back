using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduSemesterByIdQuery(int Id) : IRequest<Edu_SemestersDto?>;
