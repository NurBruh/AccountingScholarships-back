using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduSchoolsQuery : IRequest<IReadOnlyList<Edu_SchoolsDto>>;
