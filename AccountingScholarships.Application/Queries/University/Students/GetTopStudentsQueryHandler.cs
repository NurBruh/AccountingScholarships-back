using MediatR;
using AccountingScholarships.Domain.DTO.University;
using AccountingScholarships.Domain.Interfaces;

namespace AccountingScholarships.Application.Queries.University.Students;

// Handler - это класс, где происходит вызов логики из репозитория
public class GetTopStudentsQueryHandler : IRequestHandler<GetTopStudentsQuery, List<StudentWithUserDto>>
{
    private readonly IEduStudentRepository _repository;

    public GetTopStudentsQueryHandler(IEduStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<StudentWithUserDto>> Handle(GetTopStudentsQuery request, CancellationToken cancellationToken)
    {
        // Вызываем репозиторий, чтобы слой Application не зависел от базы (EF Core)
        return await _repository.GetTopStudentsWithUserAsync(request.TopCount, cancellationToken);
    }
}
