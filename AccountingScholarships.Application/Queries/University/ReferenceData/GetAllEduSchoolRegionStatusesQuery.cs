using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduSchoolRegionStatusesQuery : IRequest<IReadOnlyList<Edu_SchoolRegionStatusesDto>>;
