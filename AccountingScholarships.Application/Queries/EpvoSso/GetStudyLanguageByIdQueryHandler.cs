using AccountingScholarships.Application.DTO.EpvoSso;
using AccountingScholarships.Domain.Entities.Real.epvosso;
using AccountingScholarships.Application.Interfaces;
using MediatR;

namespace AccountingScholarships.Application.Queries.EpvoSso;

public class GetStudyLanguageByIdQueryHandler
    : IRequestHandler<GetStudyLanguageByIdQuery, StudyLanguagesDto?>
{
    private readonly IEpvoSsoRepository<StudyLanguages> _repository;

    public GetStudyLanguageByIdQueryHandler(IEpvoSsoRepository<StudyLanguages> repository)
    {
        _repository = repository;
    }

    public async Task<StudyLanguagesDto?> Handle(
        GetStudyLanguageByIdQuery request, CancellationToken cancellationToken)
    {
        var s = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (s is null) return null;

        return new StudyLanguagesDto
        {
            Id = s.Id,
            Center_StudyLang_Id = s.Center_StudyLang_Id,
            NameEn = s.NameEn,
            NameKz = s.NameKz,
            NameRu = s.NameRu,
        };
    }
}
