using AccountingScholarships.Domain.DTO.University;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public record GetAllStudentInfoTranslationsQuery : IRequest<IReadOnlyList<StudentInfo_TranslationsDto>>;
