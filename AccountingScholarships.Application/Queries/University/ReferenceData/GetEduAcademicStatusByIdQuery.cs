using AccountingScholarships.Domain.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduAcademicStatusByIdQuery(int Id) : IRequest<Edu_AcademicStatusesDto?>;
