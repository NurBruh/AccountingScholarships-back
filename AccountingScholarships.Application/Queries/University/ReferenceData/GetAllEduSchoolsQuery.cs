using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduSchoolsQuery : IRequest<IReadOnlyList<Edu_SchoolsDto>>;
