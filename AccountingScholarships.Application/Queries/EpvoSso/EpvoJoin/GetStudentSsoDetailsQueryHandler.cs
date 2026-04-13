using AccountingScholarships.Application.DTO.EpvoSso.EpvoJoin;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso.EpvoJoin;

public class GetStudentSsoDetailsQueryHandler
    : IRequestHandler<GetStudentSsoDetailsQuery, IReadOnlyList<StudentSsoDetailDto>>
{
    private readonly ISsoStudentDetailsRepository _repository;

    public GetStudentSsoDetailsQueryHandler(ISsoStudentDetailsRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<StudentSsoDetailDto>> Handle(
        GetStudentSsoDetailsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}
