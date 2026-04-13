using MediatR;
using AccountingScholarships.Application.DTO;
using AccountingScholarships.Application.Interfaces;

namespace AccountingScholarships.Application.Queries.University.Students;

public class GetAllEduStudentsQueryHandler : IRequestHandler<GetAllEduStudentsQuery, IReadOnlyList<EduStudentDto>>
{
    private readonly IEduStudentRepository _repository;

    public GetAllEduStudentsQueryHandler(IEduStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EduStudentDto>> Handle(GetAllEduStudentsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsDtoAsync(cancellationToken);
    }
}
