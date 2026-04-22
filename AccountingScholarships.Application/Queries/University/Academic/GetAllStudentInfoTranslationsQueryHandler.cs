using AccountingScholarships.Application.DTO.University;
using AccountingScholarships.Domain.Entities.Real.university;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.University.Academic;

public class GetAllStudentInfoTranslationsQueryHandler : IRequestHandler<GetAllStudentInfoTranslationsQuery, IReadOnlyList<StudentInfo_TranslationsDto>>
{
    private readonly ISsoRepository<StudentInfo_Translations> _repository;

    public GetAllStudentInfoTranslationsQueryHandler(ISsoRepository<StudentInfo_Translations> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<StudentInfo_TranslationsDto>> Handle(GetAllStudentInfoTranslationsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => new StudentInfo_TranslationsDto
        {
            TableName = e.TableName,
            ColumnName = e.ColumnName,
            ObjectID = e.ObjectID,
            Language = e.Language,
            Value = e.Value
        }).ToList().AsReadOnly();
    }
}
