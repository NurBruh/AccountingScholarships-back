using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduEntrantStatusesQuery : IRequest<IReadOnlyList<Edu_EntrantStatusesDto>>;
