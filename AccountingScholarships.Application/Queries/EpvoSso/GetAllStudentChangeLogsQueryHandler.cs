using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Application.Interfaces;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllStudentChangeLogsQueryHandler
    : IRequestHandler<GetAllStudentChangeLogsQuery, IReadOnlyList<StudentChangeLogDto>>
{
    private readonly IEpvoSsoRepository<StudentChangeLog> _repository;

    public GetAllStudentChangeLogsQueryHandler(IEpvoSsoRepository<StudentChangeLog> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<StudentChangeLogDto>> Handle(
        GetAllStudentChangeLogsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new StudentChangeLogDto
        {
            Id            = s.Id,
            IinPlt        = s.IinPlt,
            FieldName     = s.FieldName,
            OldValue      = s.OldValue,
            NewValue      = s.NewValue,
            DataSource    = s.DataSource,
            ChangedAt     = s.ChangedAt,
            ChangedBy     = s.ChangedBy,
            SyncSessionId = s.SyncSessionId,
        }).ToList().AsReadOnly();
    }
}
