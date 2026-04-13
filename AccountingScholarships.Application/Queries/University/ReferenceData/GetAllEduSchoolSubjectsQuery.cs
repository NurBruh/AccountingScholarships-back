using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduSchoolSubjectsQuery : IRequest<IReadOnlyList<Edu_SchoolSubjectsDto>>;
