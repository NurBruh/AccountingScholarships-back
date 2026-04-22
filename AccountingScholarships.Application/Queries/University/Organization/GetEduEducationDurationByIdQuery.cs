using AccountingScholarships.Application.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public record GetEduEducationDurationByIdQuery(int Id) : IRequest<Edu_EducationDurationsDto?>;
