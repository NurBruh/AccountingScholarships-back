using AccountingScholarships.Application.DTO.University;
using MediatR;
namespace AccountingScholarships.Application.Queries.University.ReferenceData;
public record GetEduSchoolRegionStatusByIdQuery(int Id) : IRequest<Edu_SchoolRegionStatusesDto?>;
