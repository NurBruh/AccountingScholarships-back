using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetAllEduAcademicStatusesQuery : IRequest<IReadOnlyList<Edu_AcademicStatusesDto>>;
