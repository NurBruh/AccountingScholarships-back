using MediatR;
using AccountingScholarships.Application.DTO;
using AccountingScholarships.Application.Interfaces;

namespace AccountingScholarships.Application.Queries.University.Students;

public class GetEduStudentByIdQueryHandler : IRequestHandler<GetEduStudentByIdQuery, EduStudentDto?>
{
    private readonly IEduStudentRepository _repository;

    public GetEduStudentByIdQueryHandler(IEduStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<EduStudentDto?> Handle(GetEduStudentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAsDtoAsync(request.StudentId, cancellationToken);
    }
}
