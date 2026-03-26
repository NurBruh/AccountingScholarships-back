using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduStudentStatusesQuery : IRequest<IReadOnlyList<Edu_StudentStatusesDto>>;
