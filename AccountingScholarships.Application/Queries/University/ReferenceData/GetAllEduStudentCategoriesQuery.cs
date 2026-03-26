using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetAllEduStudentCategoriesQuery : IRequest<IReadOnlyList<Edu_StudentCategoriesDto>>;
