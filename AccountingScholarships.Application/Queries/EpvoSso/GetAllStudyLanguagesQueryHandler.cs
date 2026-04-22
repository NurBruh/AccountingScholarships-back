using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetAllStudyLanguagesQueryHandler
    : IRequestHandler<GetAllStudyLanguagesQuery, IReadOnlyList<StudyLanguagesDto>>
{
    private readonly IEpvoSsoRepository<StudyLanguages> _repository;

    public GetAllStudyLanguagesQueryHandler(IEpvoSsoRepository<StudyLanguages> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<StudyLanguagesDto>> Handle(
        GetAllStudyLanguagesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(s => new StudyLanguagesDto
        {
            Id = s.Id,
            Center_StudyLang_Id = s.Center_StudyLang_Id,
            NameRu = s.NameRu,
            NameKz = s.NameKz,
            NameEn = s.NameEn,
        }).ToList().AsReadOnly();
    }
}
