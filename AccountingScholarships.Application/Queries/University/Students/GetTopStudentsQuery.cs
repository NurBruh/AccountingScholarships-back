using MediatR;
using AccountingScholarships.Domain.DTO.University;

namespace AccountingScholarships.Application.Queries.University.Students;

// Query - это класс-команда, который говорит ЧТО мы хотим получить.
// IRequest<List<StudentWithUserDto>> означает, что в результате мы ожидаем список DTO.
public class GetTopStudentsQuery : IRequest<List<StudentWithUserDto>>
{
    // Сюда можно добавлять параметры фильтрации, например:
    public int TopCount { get; set; } = 1000;
}
