using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetEduSchoolSubjectByIdQuery(int Id) : IRequest<Edu_SchoolSubjectsDto?>;
