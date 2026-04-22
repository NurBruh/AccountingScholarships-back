using MediatR;
using AccountingScholarships.Application.DTO;
using AccountingScholarships.Application.Interfaces;

namespace AccountingScholarships.Application.Queries.University.Students;

public class GetEduStudentByIINQueryHandler : IRequestHandler<GetEduStudentByIINQuery, EduStudentDto?>
{
    private readonly IEduStudentRepository _repository;

    public GetEduStudentByIINQueryHandler(IEduStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<EduStudentDto?> Handle(GetEduStudentByIINQuery request, CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIINAsync(request.IIN, cancellationToken);
        if (student is null)
            return null;

        return await _repository.GetAsDtoAsync(student.StudentID, cancellationToken);
    }
}
