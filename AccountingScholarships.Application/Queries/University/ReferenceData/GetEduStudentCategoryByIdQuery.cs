using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.ReferenceData;

public record GetEduStudentCategoryByIdQuery(int Id) : IRequest<Edu_StudentCategoriesDto?>;
