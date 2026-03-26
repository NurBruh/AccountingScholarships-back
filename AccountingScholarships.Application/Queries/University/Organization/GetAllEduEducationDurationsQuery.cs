using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Organization;

public record GetAllEduEducationDurationsQuery : IRequest<IReadOnlyList<Edu_EducationDurationsDto>>;
