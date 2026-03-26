using AccountingScholarships.Domain.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduSchoolRegionStatusByIdQuery(int Id) : IRequest<Edu_SchoolRegionStatusesDto?>;
