using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetEduStudentStatusByIdQuery(int Id) : IRequest<Edu_StudentStatusesDto?>;
